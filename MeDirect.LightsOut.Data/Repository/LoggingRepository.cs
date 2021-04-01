using Dapper;
using MeDirect.LightsOut.Data.Model.Logging;
using MeDirect.LightsOut.Data.Settings;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Data.Repository
{
    /// <summary>
    /// Logging Repository
    /// </summary>
    public class LoggingRepository : ILoggingRepository
    {
        #region Local Variables
        private readonly IApiConfiguration _apiConfiguration;
        #endregion Local Variables

        #region Constructor
        /// <summary>
        /// Default Constuctor
        /// </summary>
        public LoggingRepository(IApiConfiguration apiConfiguration)
        {
            _apiConfiguration = apiConfiguration;
        }
        #endregion Constructor

        /// <summary>
        /// Store the request response model in the database logs schema
        /// </summary>
        /// <param name="requestResponse"></param>
        /// <returns>rows affected</returns>
        public async Task<int> LogRequestAsync(RequestResponse requestResponse)
        {
            string sql = @"
                    INSERT [logs].[RequestResponse]
                               ([RequestId]
                               ,[DateTime]
                               ,[Flow]
                               ,[Host]
                               ,[Method]
                               ,[QueryString]
                               ,[Body]
                               ,[TimeMs]) 
                    VALUES (@RequestSessionId, 
                            @DateTime,
                            @Flow, 
                            @Host, 
                            @Method, 
                            @QueryString, 
                            @Body,
                            @TimeMs)";

            // note: Dapper automatically manages Opening and Closing of SQL Connections
            using var dbConnection = new SqlConnection(_apiConfiguration.ConnectionString());
            return await dbConnection.ExecuteAsync(sql,requestResponse);
        }
    }
}
