using System;
using System.Collections.Generic;

namespace AOC._2018.Utility
{
    public class StringHelper
    {
        public static int LetterDifferenceCount(string string1, string string2, bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(string1) || string.IsNullOrWhiteSpace(string2))
            {
                return Math.Abs(string1?.Length ?? 0 - string2?.Length ?? 0);
            }

            var difference = Math.Abs(string1.Length - string2.Length);
            var loopIterations = Math.Min(string1.Length, string2.Length);

            var comparsionType = caseSensitive
                ? StringComparison.InvariantCulture
                : StringComparison.InvariantCultureIgnoreCase;

            for (var i = 0; i < loopIterations; i++)
            {
                if (!string.Equals(string1[i].ToString(), string2[i].ToString(), comparsionType))
                {
                    difference++;
                }
            }

            return difference;
        }

        public static string GetOrderedStringIntersection(string string1, string string2, bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(string1) || string.IsNullOrWhiteSpace(string2))
            {
                return "";
            }

            var comparsionType = caseSensitive
                ? StringComparison.InvariantCulture
                : StringComparison.InvariantCultureIgnoreCase;
            var similarCharacters = new List<char>();

            var loopIterations = Math.Min(string1.Length, string2.Length);
            for (var i = 0; i < loopIterations; i++)
            {
                if (string.Equals(string1[i].ToString(), string2[i].ToString(), comparsionType))
                {
                    similarCharacters.Add(string1[i]);
                }
            }

            return string.Concat(similarCharacters);
        }
    }
}