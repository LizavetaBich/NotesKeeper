using System;
using System.Collections.Generic;

namespace NotesKeeper.Common
{
    public class Day : IEquatable<Day>
    {
        private DateTime _day;

        public Day(DateTime dateTime)
        {
            this._day = dateTime;
            this.Events = new List<CustomEvent>();
        }

        public int DayNumber => _day.Day;

        public int Month => _day.Month;

        public int Year => _day.Year;

        public DayOfWeek DayOfTheWeek => _day.DayOfWeek;

        public IEnumerable<CustomEvent> Events { get; set; }

        public bool Equals(Day other)
        {
            return other != null
                && other.DayNumber == this.DayNumber
                && other.Month == this.Month
                && other.Year == this.Year;
        }

        public static implicit operator Day(DateTime dateTime)
        {
            return new Day(dateTime);
        }

        public static explicit operator DateTime(Day day)
        {
            if (day == null)
            {
                throw new ArgumentNullException("");
            }

            return new DateTime(day.Year, day.Month, day.DayNumber);
        }

        public static bool operator ==(Day first, Day second)
        {
            if (first == null && second == null)
            {
                return true;
            }

            return first == null ? second.Equals(first) : first.Equals(second);
        }

        public static bool operator !=(Day first, Day second)
        {
            if (first == null && second == null)
            {
                return false;
            }

            return first == null ? !second.Equals(first) : !first.Equals(second);
        }
    }
}
