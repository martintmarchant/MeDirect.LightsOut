using System;

namespace MeDirect.LightsOut.Data.Model.Logging
{
    /// <summary>
    /// Request Response Model
    /// to hold raw request response detail
    /// </summary>
    public class RequestResponse
    {
        /// <summary>
        /// Valid RequestId
        /// </summary>
        public Guid RequestSessionId { get; set; }

        /// <summary>
        /// DateTime of the action
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Request 1/Response 2
        /// </summary>
        public short Flow { get; set; }
        
        /// <summary>
        /// Host Name
        /// </summary>
        public string Host { get; set; }
        
        /// <summary>
        /// Method Called
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// Supplied QueryString Parameters (Raw)
        /// </summary>
        public string QueryString { get; set; }
        /// <summary>
        /// Supplied Body Content (Raw)
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Process Time Taken
        /// </summary>
        public long TimeMs { get; set; }
    }
}
