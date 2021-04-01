using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeDirect.Common.Tests
{
    public class Common_StringExtensions_Tests
    { 
        /// <summary>
        /// checks the output from astream i
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task StreamRead_result_matches_input_stream()
        {
            // Arrange
            string testString = "This is a test";
            // Act
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(testString));
            
            // Assert
            Assert.Equal(testString, await stream.ConvertToStringAsync());
        }

        /// <summary>
        /// checks the output doesnt fail with empty string
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task StreamRead_result_empty()
        {
            // Arrange
            string testString = string.Empty;
            // Act
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(testString));

            // Assert
            Assert.Equal(string.Empty, await stream.ConvertToStringAsync());
        }
    }
}
