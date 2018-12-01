using System.Collections.Generic;
using System.Linq;

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
    }
}