using System;
using System.Collections.Generic;

namespace NotesKeeper.Common
{
    public class Day
    {
        private DateTime _day;

        public Day(DateTime dateTime)
        {
            this._day = dateTime;
            this.Events = new List<Event>();
        }

        public int DayNumber => _day.Day;

        public int Month => _day.Month;

        public int Year => _day.Year;

        public DayOfWeek DayOfTheWeek => _day.DayOfWeek;

        public IEnumerable<CustomEvent> Events { get; set; }

        public static explicit operator Day(DateTime dateTime)
        {
            return new Day(dateTime);
        }

        public static implicit operator DateTime(Day day)
        {
            if (day == null)
            {
                throw new ArgumentNullException("");
            }

            return new DateTime(day.Year, day.Month, day.DayNumber);
        }
    }
}
