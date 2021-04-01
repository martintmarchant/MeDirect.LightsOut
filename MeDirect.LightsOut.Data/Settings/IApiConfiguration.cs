using System;

namespace MeDirect.LightsOut.Data.Settings
{
    public interface IApiConfiguration
    {
        Guid RequestPipelineGuid { get; set; }
        int CacheTimeInterval();
        string ConnectionString();
        bool GetRequestResponseArchiveEnabled();
    }
}