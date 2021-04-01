using MeDirect.LightsOut.Data.Model.Logging;
using System.Threading.Tasks;

namespace MeDirect.LightsOut.Data.Repository
{
    /// <summary>
    /// Logging Repository
    /// </summary>
    public interface ILoggingRepository
    {
        /// <summary>
        /// saves the Request/Response stream (called from middleware).
        /// </summary>
        /// <param name="requestResponse"></param>
        /// <returns></returns>
        Task<int> LogRequestAsync(RequestResponse requestResponse);
    }
}