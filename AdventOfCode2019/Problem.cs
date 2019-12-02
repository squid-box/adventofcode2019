namespace AdventOfCode2019
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Base for all problem solutions.
    /// </summary>
    public abstract class Problem
    {
        /// <summary>
        /// The number (id) of this problem.
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// Gets the input of this problem.
        /// </summary>
        public string[] Input => File.ReadAllLines($"Input{Path.DirectorySeparatorChar}{Number}.input")
            .Where(l => !string.IsNullOrEmpty(l)).ToArray();

        protected Problem(int number)
        {
            Number = number;
        }

        public abstract string Answer();

        /// <summary>
        /// Returns both solutions of this problem in a human-readable format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Problem #{Number}:{Environment.NewLine}{Answer()}";
        }
    }
}
