using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using AOC._2018.Models;

namespace AOC._2018
{
    public static class DataImport
    {
        public static IEnumerable<int> Day1Data()
        {
            var lines = System.IO.File.ReadAllLines(@"..\AOC.2018\Data\day1input.txt");

            var linesAsNumbers = lines.Select(int.Parse);

            return linesAsNumbers;
        }

        public static IEnumerable<string> Day2Data()
        {
            var lines = System.IO.File.ReadAllLines(@"..\AOC.2018\Data\day2input.txt");

            return lines;
        }

        /// <summary>
        /// Parsing a set of strings into objects
        /// #1332 @ 303,941: 11x18
        /// #id @ x,y: Height x Width
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GeoBlock> Day3Data()
        {
            var lines = System.IO.File.ReadAllLines(@"..\AOC.2018\Data\day3input.txt");
            var geoBlocks = new List<GeoBlock>();

            foreach (var line in lines)
            {
                var parts = line.Split(" ");
                var coordinates = parts[2].Replace(":", "").Split(",");
                var size = parts[3].Split("x");

                if (int.TryParse(parts[0].Substring(1), out var id) &&
                    int.TryParse(coordinates[0], out var x) &&
                    int.TryParse(coordinates[1], out var y) &&
                    int.TryParse(size[0], out var width) &&
                    int.TryParse(size[1], out var height))
                {
                    geoBlocks.Add(new GeoBlock(id, x, y, width, height));
                }
            }

            return geoBlocks;
        }
    }
}