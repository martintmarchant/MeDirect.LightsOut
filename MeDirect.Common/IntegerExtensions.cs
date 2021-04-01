using System;

namespace MeDirect.Common
{
    /// <summary>
    /// Integer Extensions class
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Calculates a random Integer.
        /// </summary>
        /// <param name="maxNumber">Highest number supported</param>
        /// <param name="minNumber">Lowest number supported</param>
        /// <returns>Randomly generated number</returns>
        public static int Randomize(this int maxNumber, int minNumber = 1)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            return random.Next(minNumber, maxNumber);
        }
    }
}