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
            var sleepBlocks = SleepTimesWithGuardId(groupedByDays);

            var mostAsleepGuardId = MostAsleepGuardId(sleepBlocks);

            var mostAsleepMinute = MostAsleepMinuteByGuardId(sleepBlocks, mostAsleepGuardId).Minute;

            return
                $"Guard: {mostAsleepGuardId}, slept most during minute: {mostAsleepMinute}, sum: {mostAsleepMinute * mostAsleepGuardId}";
        }

        public static string Part2(IEnumerable<LogEvent> logEvents)
        {
            var sortedLogEvents = logEvents.OrderBy(logEvent => logEvent.DateTime).ToList();
            var groupedByDays =
                sortedLogEvents.GroupBy(logEvent =>
                    logEvent.DateTime.AddHours(1)
                        .Date); //Add 1 hour because some events occurs close to, but before 00:00
            var sleepBlocks = SleepTimesWithGuardId(groupedByDays);

            var mostAsleepGuard = new TopSleepingGuard
            {
                GuardId = 0,
                TopSleepingMinute = new TopSleepingMinute()
            };

            foreach (var blockGroup in sleepBlocks.GroupBy(block => block.GuardId))
            {
                var currentTopSleepingMinute = MostAsleepMinuteByGuardId(sleepBlocks, blockGroup.Key);

                if (currentTopSleepingMinute.Total <= mostAsleepGuard.TopSleepingMinute.Total) continue;

                mostAsleepGuard.GuardId = blockGroup.Key;
                mostAsleepGuard.TopSleepingMinute = currentTopSleepingMinute;
            }

            return
                $"Guard: {mostAsleepGuard.GuardId}, slept most during minute: {mostAsleepGuard.TopSleepingMinute.Minute}, sum: {mostAsleepGuard.GuardId * mostAsleepGuard.TopSleepingMinute.Minute}";
        }

        private static int MostAsleepGuardId(IEnumerable<Shift> guardSleepingSchedule)
        {
            var groupedById = guardSleepingSchedule.GroupBy(tuple => tuple.GuardId);

            var id = 0;
            var currentMax = 0;
            foreach (var group in groupedById)
            {
                var minutesSlept = group.Sum(block => block.Minutes.Count(minute => minute.Equals(true)));

                if (minutesSlept <= currentMax) continue;
                currentMax = minutesSlept;
                id = group.First().GuardId;
            }

            return id;
        }

        /// <summary>
        /// minute, amount of sleep in minute
        /// </summary>
        /// <param name="shifts"></param>
        /// <param name="guardId"></param>
        /// <returns></returns>
        private static TopSleepingMinute MostAsleepMinuteByGuardId(IEnumerable<Shift> shifts, int guardId)
        {
            var tuplesForGivenGuard =
                shifts.GroupBy(shift => shift.GuardId).FirstOrDefault(block => block.Key == guardId);
            if (tuplesForGivenGuard == null)
            {
                return null;
            }

            var mostAsleepMinute = new TopSleepingMinute();

            for (var min = 0; min < 60; min++)
            {
                if (tuplesForGivenGuard.Count(block => block.Minutes.ElementAt(min)) <=
                    mostAsleepMinute.Total) continue;

                mostAsleepMinute.Minute = min;
                mostAsleepMinute.Total = tuplesForGivenGuard.Count(block => block.Minutes.ElementAt(min));
            }

            return mostAsleepMinute;
        }

        private static List<Shift> SleepTimesWithGuardId(IEnumerable<IGrouping<DateTime, LogEvent>> groupedByDay)
        {
            var sleepBlocks = new List<Shift>();
            foreach (var group in groupedByDay)
            {
                var currentguardId = group.ElementAt(0).GuardId;
                var isSleeping = false;
                var sleep = new bool[60];

                for (var min = 0; min <= 59; min++)
                {
                    var currentMinute = group.Where(logEvent => !logEvent.CheckIn)
                        .FirstOrDefault(logEvent => logEvent.DateTime.Minute == min);
                    if (currentMinute != null)
                    {
                        isSleeping = currentMinute.StartSleep;
                    }

                    sleep[min] = isSleeping;
                }

                sleepBlocks.Add(new Shift
                {
                    GuardId = currentguardId,
                    Minutes = sleep
                });
            }

            return sleepBlocks;
        }
    }

    public class TopSleepingMinute
    {
        public int Minute { get; set; }
        public int Total { get; set; }
    }

    public class TopSleepingGuard
    {
        public int GuardId { get; set; }
        public TopSleepingMinute TopSleepingMinute { get; set; }
    }

    public class Shift
    {
        public int GuardId { get; set; }
        public IEnumerable<bool> Minutes { get; set; }
    }
}