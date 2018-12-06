using System;
using System.Linq;

namespace AOC._2018.Solutions
{
    public static class Day5
    {
        public static string Part1(string input)
        {
            input = React(input);

            return $"Remaining letters: {input.Length}";
        }

        public static string Part2(string input)
        {
            var smallest = input.Length;

            foreach (var letter in input.ToLowerInvariant().Distinct())
            {
                var inputWithoutLetter = input.Replace(letter.ToString(), string.Empty,
                    StringComparison.InvariantCultureIgnoreCase);

                var sizeAfterReaction = React(inputWithoutLetter).Length;
                smallest = smallest > sizeAfterReaction ? sizeAfterReaction : smallest;
            }

            return $"Smallest possible: {smallest}";
        }

        private static bool HasReaction(char first, char second)
        {
            if (!(char.IsLower(first) && char.IsUpper(second) ||
                  char.IsLower(second) && char.IsUpper(first))) return false;

            return string.Equals(
                first.ToString(),
                second.ToString(),
                StringComparison.InvariantCultureIgnoreCase);
        }

        private static string React(string input)
        {
            var morePossibleReplacements = true;
            var anyReplacementsThisRound = false;
            while (morePossibleReplacements)
            {
                for (var index = 0; index < input.Length - 1; index++)
                {
                    var currentChar = input[index];
                    var nextChar = input[index + 1];

                    if (!HasReaction(currentChar, nextChar)) continue;

                    anyReplacementsThisRound = true;
                    input = input.Substring(0, index) + input.Substring(index + 2);
                }

                morePossibleReplacements = anyReplacementsThisRound;
                anyReplacementsThisRound = !anyReplacementsThisRound;
            }

            return input;
        }
    }
}