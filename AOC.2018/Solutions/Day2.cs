using System.Collections.Generic;
using System.Linq;
using AOC._2018.Models;
using AOC._2018.Utility;

namespace AOC._2018.Solutions
{
    public class Day2
    {
        public static object Part1(IEnumerable<string> lines)
        {
            var twoLetters = 0;
            var threeLetters = 0;

            foreach (var line in lines)
            {
                var letterFrequency = new List<LetterFrequency>();

                foreach (var character in line.ToCharArray())
                {
                    var currentLetterFrequency = letterFrequency.FirstOrDefault(letter => letter.Letter == character);

                    if (currentLetterFrequency == null)
                    {
                        letterFrequency.Add(new LetterFrequency(character, 1));
                    }
                    else
                    {
                        currentLetterFrequency.Frequency++;
                    }
                }

                twoLetters += letterFrequency.Any(letter => letter.Frequency == 2) ? 1 : 0;
                threeLetters += letterFrequency.Any(letter => letter.Frequency == 3) ? 1 : 0;
            }

            return
                $"We have {twoLetters} two times, and {threeLetters} occuring three times. Their sum is {twoLetters * threeLetters} ";
        }

        public static object Part2(IEnumerable<string> lines)
        {
            var linesAsList = lines.ToList();

            for (var outer = 0; outer < linesAsList.Count(); outer++)
            {
                for (var inner = outer; inner < linesAsList.Count(); inner++)
                {
                    if (StringHelper.LetterDifferenceCount(linesAsList.ElementAtOrDefault(outer),
                            linesAsList.ElementAtOrDefault(inner)) != 1) continue;

                    var intersection = StringHelper.GetOrderedStringIntersection(
                        linesAsList.ElementAtOrDefault(outer),
                        linesAsList.ElementAtOrDefault(inner));

                    return $"The first similar IDs (single letter difference) have this in common: {intersection}";
                }
            }

            return null;
        }
    }
}