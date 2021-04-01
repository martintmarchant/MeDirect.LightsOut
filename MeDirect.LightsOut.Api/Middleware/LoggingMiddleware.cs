using MeDirect.Common;
using MeDirect.LightsOut.Data.Model.Logging;
using MeDirect.LightsOut.Data.Repository;
using MeDirect.LightsOut.Data.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Api.Middleware
{
    /// <summary>
    /// Middleware Logging
    /// following interview discussion on middleware, wanted to include something here.. 
    /// 
    /// this class is responsible for log setup/Enrichment 
    /// and (optionally) recording request/response streams 
    /// into the database (includes performance stats for performance monitoring)
    /// 
    /// Logs.RequestResponse.RequestIds in the database links to logs.Log.RequestIds 
    /// so you have full end to end tracability... 
    ///
    /// </summary>
    /// <remarks>
    /// there is still work to be done here.. 
    ///  
    /// example of work outstanding: 
    /// selection of controller/actions to log 
    /// performance:  sql fire and forget logging
    /// testing: from multiple simaltanious calls etc
    /// </remarks>
    public class LoggingMiddleware
    {
         #region Local Variables
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private ILoggingRepository _loggingRepository;
        private IApiConfiguration _apiConfiguration;

        private enum Flow { Request = 1, Response = 2 }
        #endregion Local Variables

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="next"></param>
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        #endregion

        /// <summary>
        /// Inserts Logging at the beginning and end of the pipeline
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loggingRepository"></param>
        /// <param name="apiConfiguration"></param>
        /// <returns></returns>
        public async Task Invoke(
                                HttpContext context,
                                ILoggingRepository loggingRepository,
                                IApiConfiguration apiConfiguration)
        {
            SetupLogging(apiConfiguration);
            Serilog.Log.Information("Logging Enabled");

            if (apiConfiguration.GetRequestResponseArchiveEnabled())
            {
                _apiConfiguration = apiConfiguration;
                _loggingRepository = loggingRepository;

                await LogRequestAsync(context);
                await LogResponseAsync(context);
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        /// <summary>
        /// Setup Serilog to log to SQL Server
        /// </summary>
        public void SetupLogging(IApiConfiguration apiConfiguration)
        {
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn("RequestId", SqlDbType.UniqueIdentifier)
                }
            };
            Serilog.Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("RequestId", apiConfiguration.RequestPipelineGuid)
                .WriteTo.MSSqlServer(
                            apiConfiguration.ConnectionString(),
                            sinkOptions: new MSSqlServerSinkOptions { SchemaName = "logs", TableName = "Log" },
                            null, null,
                            LogEventLevel.Information, null,
                            columnOptions: columnOptions, null, null)
                .CreateLogger();
        }

        /// <summary>
        /// Logs the raw request info into the repository
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task LogRequestAsync(HttpContext context)
        {
            context.Request.EnableBuffering();  // enables stream to be reused
            using var requestStream = _recyclableMemoryStreamManager.GetStream();

            await context.Request.Body.CopyToAsync(requestStream);
            await Log(context, await requestStream.ConvertToStringAsync(), Flow.Request);

            context.Request.Body.Position = 0;
        }

        /// <summary>
        /// Logs the raw response info into the repository
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task LogResponseAsync(HttpContext context)
        {
            var bodyContentStream = context.Response.Body;

            using var tempResponse = new MemoryStream();
            context.Response.Body = tempResponse;

            // add metrix to pipeline and continue (reponse body will be placed in the responseTempStream)
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            await _next.Invoke(context);
            watch.Stop();

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = await new StreamReader(context.Response.Body).ReadToEndAsync();

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            // Write to Repository
            await Log(context, body, Flow.Response, watch.ElapsedMilliseconds);

            // reset original stream
            await tempResponse.CopyToAsync(bodyContentStream);
        }

        /// <summary>
        /// write to database request/response stream
        /// </summary>
        /// <param name="context"></param>
        /// <param name="body"></param>
        /// <param name="flowDirection"></param>
        /// <param name="timeMs"></param>
        /// <returns></returns>
        private async Task Log(HttpContext context, string body, Flow flowDirection, long timeMs = 0)
        {
            try
            {
                await _loggingRepository.LogRequestAsync(new RequestResponse()
                {
                    RequestSessionId = _apiConfiguration.RequestPipelineGuid,
                    DateTime = DateTime.Now,
                    Host = context.Request.Host.ToString(),
                    Method = context.Request.Path,
                    Flow = (short)flowDirection,
                    QueryString = context.Request.QueryString.ToString(),
                    Body = body.Replace("'", "`"),
                    TimeMs = timeMs
                });
            }
            catch (Exception ex)
            {
                // dont want to fail response if logging fails - but records error in logs (this will never generate an error)
                Serilog.Log.Error($"Saving {flowDirection} Stream - Failed with the following exception: {ex.Message}, Execution Continued.");
            }
        }
    }
}

