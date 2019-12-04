namespace AdventOfCode2019.Problems
{
    using System;
    using System.Linq;

    internal class Problem4 : Problem
    {
        public Problem4() : base(4)
        {
        }

        internal static Tuple<int, int> ParseInput(string input)
        {
            var split = input.Trim().Split('-');

            return new Tuple<int, int>(int.Parse(split[0]), int.Parse(split[1]));
        }

        internal static bool IsValidPassword(int password)
        {
            var passwordString = password.ToString();

            if (passwordString.Length != 6)
            {
                return false;
            }

            var digits = passwordString.Select(i => int.Parse(i.ToString())).ToArray();

            var previousDigit = -1;
            var hasRepeatingDigits = false;

            for (var i = 0; i < digits.Length; i++)
            {
                var current = digits[i];

                if (current < previousDigit)
                {
                    return false;
                }

                if (current.Equals(previousDigit))
                {
                    hasRepeatingDigits = true;
                }

                previousDigit = current;
            }

            return hasRepeatingDigits;
        }

        internal static bool IsValidPasswordPart2(int password)
        {
            var passwordString = password.ToString();

            if (passwordString.Length != 6)
            {
                return false;
            }

            var digits = passwordString.Select(i => int.Parse(i.ToString())).ToArray();

            var previousDigit = -1;
            var hasRepeatingDigits = false;
            var digitRepeats = 1;

            for (var i = 0; i < digits.Length; i++)
            {
                var current = digits[i];

                // Ensure we're never decreasing.
                if (current < previousDigit)
                {
                    return false;
                }

                // Check for groups of repeated characters.
                if (current.Equals(previousDigit))
                {
                    digitRepeats++;

                    if (i == digits.Length-1 && digitRepeats == 2)
                    {
                        hasRepeatingDigits = true;
                    }
                }
                else
                {
                    if (digitRepeats == 2)
                    {
                        hasRepeatingDigits = true;
                    }

                    digitRepeats = 1;
                }

                previousDigit = current;
            }

            return hasRepeatingDigits;
        }

        public override string Answer()
        {
            var validPasswords = 0;
            var validPasswordsPartTwo = 0;

            var (lowerLimit, upperLimit) = ParseInput(Input[0]);

            for (var password =  lowerLimit; password <= upperLimit; password++)
            {
                if (IsValidPassword(password))
                {
                    validPasswords++;

                    if (IsValidPasswordPart2(password))
                    {
                        validPasswordsPartTwo++;
                    }
                }
            }

            return $"Part 1: {validPasswords}\nPart 2: {validPasswordsPartTwo}";
        }
    }
}