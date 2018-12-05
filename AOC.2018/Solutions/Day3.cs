using System.Collections.Generic;
using System.Linq;
using AOC._2018.Models;

namespace AOC._2018.Solutions
{
    public class Day3
    {
        public static string Part1(IEnumerable<GeoBlock> geoBlocks)
        {
            var container = Setup(geoBlocks);

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

        public static string Part2(IEnumerable<GeoBlock> geoBlocks)
        {
            var blocks = geoBlocks.ToList();
            var container = Setup(blocks);

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
        
        private static GeoContainer Setup(IEnumerable<GeoBlock> geoBlocks)
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