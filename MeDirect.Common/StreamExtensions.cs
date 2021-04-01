using System.IO;
using System.Threading.Tasks;

namespace MeDirect.Common
{
    /// <summary>
    /// Stream Extensions Class
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Converts a stream to a string
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async static Task<string> ConvertToStringAsync(this Stream stream)
        {
            if (!stream.CanSeek)
                return string.Empty;

            stream.Seek(0, SeekOrigin.Begin);

            using StreamReader reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }    
    }
}
