using System;

namespace AOC._2018.Models
{
    public class LogEvent
    {
        public DateTime DateTime { get; set; }
        public bool StartSleep { get; set; }
        public bool EndSleep { get; set; }
        public bool CheckIn { get; set; }

        public int GuardId { get; set; }
    }
}