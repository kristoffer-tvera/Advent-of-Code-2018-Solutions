using System.Collections.Generic;
using System.Linq;
using AOC._2018.Models;
using AOC._2018.Utility;

namespace AOC._2018
{
    public static class Solutions
    {
        public static string Day1Part1(IEnumerable<int> values)
        {
            var enumerable = values.ToList();
            if (!enumerable.Any()) return "Invalid input";

            var sum = 0;
            foreach (var num in enumerable)
            {
                sum += num;
            }

            return $"The sum is {sum}";
        }

        public static string Day1Part2(IEnumerable<int> values)
        {
            var enumerable = values.ToList();
            if (!enumerable.Any()) return null;

            var sum = 0;
            var sums = new List<int>() {sum};
            var index = 0;
            while (sums.Count < enumerable.Count * enumerable.Count * enumerable.Count)
            {
                sum += enumerable.ElementAt(index % enumerable.Count);

                if (sums.Contains(sum))
                {
                    return $"We first reached {sum} twice";
                }

                sums.Add(sum);
                index++;
            }

            return null;
        }

        public static object Day2Part1(IEnumerable<string> lines)
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

        public static object Day2Part2(IEnumerable<string> lines)
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

        public static string Day3Part1(IEnumerable<GeoBlock> geoBlocks)
        {
            var container = Day3Setup(geoBlocks);

            var duplicates = 0;

            for (var y = 0; y < container.Height; y++)
            {
                for (var x = 0; x < container.Width; x++)
                {
                    duplicates += container.Map[y, x] > 1 ? 1 : 0;
                }
            }

            return $"{duplicates} was placed in existing tiles";
        }

        public static string Day3Part2(IEnumerable<GeoBlock> geoBlocks)
        {
            var blocks = geoBlocks.ToList();
            var container = Day3Setup(blocks);

            foreach (var block in blocks)
            {
                int CheckForIntersections()
                {
                    for (var y = block.Y; y < block.Y + block.Height; y++)
                    {
                        for (var x = block.X; x < block.X + block.Width; x++)
                        {
                            if (container.Map[y, x] > 1)
                                return 0;
                        }
                    }

                    return block.Id;
                }

                if (CheckForIntersections() > 0)
                    return $"#{block.Id} is the only area that does not overlap";
            }

            return $"Found no overlap";
        }

        private static GeoContainer Day3Setup(IEnumerable<GeoBlock> geoBlocks)
        {
            var blocks = geoBlocks.ToList();
            var width = blocks.Max(block => block.X + block.Width);
            var height = blocks.Max(block => block.Y + block.Height);

            var container = new GeoContainer(width, height);

            foreach (var block in blocks)
            {
                for (var y = block.Y; y < block.Y + block.Height; y++)
                {
                    for (var x = block.X; x < block.X + block.Width; x++)
                    {
                        container.Map[y, x]++;
                    }
                }
            }

            return container;
        }
    }
}