namespace AdventOfCode2019.Utils
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Utilities for working on collections.
    /// </summary>
    public static class CollectionUtilities
    {
        /// <summary>
        /// Returns all possible permutations of a given enumerable collection.
        /// </summary>
        /// <remarks>Taken from https://stackoverflow.com/a/10630026 and lightly modified.</remarks>
        /// <typeparam name="T">Type of data in collection.</typeparam>
        /// <param name="input">The collection to return permutations for.</param>
        /// <param name="length">Length of input.</param>
        /// <returns>All possible permutations of the given collection.</returns>
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> input, int length = -1)
        {
            switch (length)
            {
                case 1:
                    return input.Select(t => new[] { t });
                case -1:
                    length = input.ToArray().Length;
                    break;
            }

            return GetPermutations(input, length - 1)
                .SelectMany(t => input.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new [] { t2 }));
        }
    }
}
