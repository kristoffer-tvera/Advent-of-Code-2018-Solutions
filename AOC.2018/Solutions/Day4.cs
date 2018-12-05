using System;
using System.Collections.Generic;
using System.Linq;
using AOC._2018.Models;

namespace AOC._2018.Solutions
{
    public class Day4
    {
        public static string Part1(IEnumerable<LogEvent> logEvents)
        {
            var sortedLogEvents = logEvents.OrderBy(logEvent => logEvent.DateTime).ToList();
            var groupedByDays =
                sortedLogEvents.GroupBy(logEvent =>
                    logEvent.DateTime.AddHours(1)
                        .Date); //Add 1 hour because some events occurs close to, but before 00:00
            var sleepBlocks = new List<Tuple<int, bool[]>>();

            foreach (var groupedByDay in groupedByDays)
            {
                var currentguardId = groupedByDay.ElementAt(0).GuardId;
                var isSleeping = false;
                var sleep = new bool[60];

                for (var min = 0; min <= 59; min++)
                {
                    var currentMinute = groupedByDay.Where(logEvent => !logEvent.CheckIn).FirstOrDefault(logEvent => logEvent.DateTime.Minute == min );
                    if (currentMinute != null)
                    {
                        isSleeping = currentMinute.StartSleep;
                    }
                    sleep[min] = isSleeping;
                }
                
                sleepBlocks.Add(new Tuple<int, bool[]>(currentguardId, sleep));
            }

            var groupedByGuardId = sleepBlocks.GroupBy(block => block.Item1).ToList();

            var guardId = 0;
            var guardTotalSleepForMinute = 0;
            foreach (var group in groupedByGuardId)
            {
                var minutesSlept = group.Sum(block => block.Item2.Count(minute => minute.Equals(true)));

                if (minutesSlept <= guardTotalSleepForMinute) continue;
                guardTotalSleepForMinute = minutesSlept;
                guardId = group.First().Item1;
            }

            var biggestSleeperGroup = groupedByGuardId.First(group => group.Key == guardId);
            var sleepMinute = 0;
            var sleepMinuteCount = 0;

            for (var min = 0; min < 60; min++)
            {
                if (biggestSleeperGroup.Count(block => block.Item2[min]) <= sleepMinuteCount) continue;
                
                sleepMinuteCount = biggestSleeperGroup.Count(block => block.Item2[min]);
                sleepMinute = min;
                
            }
            
            return $"Guard: {guardId}, slept most during minute: {sleepMinute}, sum: {sleepMinute * guardId}";
        }

        public static string Part2(IEnumerable<LogEvent> logEvents)
        {
            return null;
        }
    }
}