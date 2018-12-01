using System;

namespace AOC._2018
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var day1Part1 = Solutions.Day1Part1(DataImport.Day1Data());
            Console.WriteLine($"Day 1, part 1: {day1Part1}");
            
            var day1Part2 = Solutions.Day1Part2(DataImport.Day1Data());
            Console.WriteLine($"Day 1, part 2: {day1Part2}");
        }
    }
}