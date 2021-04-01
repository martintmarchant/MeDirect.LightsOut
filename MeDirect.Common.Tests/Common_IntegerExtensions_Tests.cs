using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace MeDirect.Common.Tests
{
    /// <summary>
    /// test the common integer extensions class
    /// </summary>
    public class Common_IntegerExtensions_Tests
    {
        #region Common Randomize
        /// <summary>
        /// does the randomize method generate a value in the expected range of values
        /// </summary>
        [Fact]
        public void Random_Number_inbetween_min_and_max_range()
        {
            // Arrange
            int minnumber = 10;
            int maxnumber = 20;

            // Act 
            var result = maxnumber.Randomize(minnumber);

            // Assert - result in the given range
            Assert.InRange(result, minnumber, maxnumber);
        }


        /// <summary>
        /// does the randomize method generate different values
        /// </summary>
        [Fact]
        public void RandomNumber_different_when_called()
        {
            // Arrange
            int minnumber = 10;
            int maxnumber = 20;

            // Act 
            var list = new List<int>();
            list.AddRange(Enumerable.Range(0, 20)
                .Select(f=> maxnumber.Randomize(minnumber) ));

            // Assert - check all numbers not the same
            Assert.False(list.GroupBy(o => o).Count() == 1);
        }

        /// <summary>
        /// does the randomize method generate a value in the expected range of values
        /// </summary>
        [Fact]
        public void RandomNumber_Results_in_expected_number()
        {
            // Arrange
            int minnumber = 10;
            int maxnumber = 10;

            // Act 
            var result = maxnumber.Randomize(minnumber);

            // Assert - random number between 10 and 10 should be 10
            Assert.Equal(10, result);
        }

        /// <summary>
        /// parameters incorrect
        /// </summary>
        [Fact]
        public void RandomNumber_minNumber_greater_then_max()
        {
            // Arrange
            int minnumber = 20;
            int maxnumber = 10;

            // Act 
            // Assert - should throw an exception
            Assert.Throws<System.ArgumentOutOfRangeException>(() => maxnumber.Randomize(minnumber));
        }
        #endregion
    }
}