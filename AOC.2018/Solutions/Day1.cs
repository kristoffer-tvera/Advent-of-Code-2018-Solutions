using System.Collections.Generic;
using System.Linq;

namespace AOC._2018.Solutions
{
    public class Day1
    {
        public static string Part1(IEnumerable<int> values)
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

        public static string Part2(IEnumerable<int> values)
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
    }
}