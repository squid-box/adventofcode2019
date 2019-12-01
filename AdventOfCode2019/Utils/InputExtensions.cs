namespace AdventOfCode2019.Utils
{
    using System.Linq;

    /// <summary>
    /// Extensions for the problem input.
    /// </summary>
    public static class InputExtensions
    {
        /// <summary>
        /// Converts the input from a collection of strings to a collection of integers.
        /// </summary>
        /// <param name="input">Input to convert.</param>
        /// <returns>An array of integers.</returns>
        public static int[] ConvertToInt(this string[] input)
        {
            return input.Select(int.Parse).ToArray();
        }
    }
}
