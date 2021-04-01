using Microsoft.Extensions.Configuration;
using System;

namespace MeDirect.LightsOut.Data.Settings
{
    /// <summary>
    /// Class to contain setting information for request pipeline
    /// Settings retreived from AppSettings.json
    /// </summary>
    public class ApiConfiguration : IApiConfiguration
    {
        #region Local Variables
        private readonly IConfiguration _configuration;
        #endregion Local Variables

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public ApiConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            RequestPipelineGuid = Guid.NewGuid();
        }
        #endregion

        /// <summary>
        /// RequestPipelineGuid (UniqueId based on the request)
        /// </summary>
        public Guid RequestPipelineGuid { get; set; }

        /// <summary>
        /// Database Connection String
        /// </summary>
        public string ConnectionString() => _configuration.GetConnectionString("MeDirectLightsOutDb");

        /// <summary>
        /// Enable Request/Response logs
        /// </summary>
        public bool GetRequestResponseArchiveEnabled() => _configuration.GetValue<bool>("RequestResponseArchiveEnabled");
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CacheTimeInterval() => _configuration.GetValue<int>("CacheTimeInterval");
    }
}
