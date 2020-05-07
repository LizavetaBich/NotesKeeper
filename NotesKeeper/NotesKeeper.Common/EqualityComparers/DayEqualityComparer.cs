using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.EqualityComparers
{
    public class DayEqualityComparer : IEqualityComparer<Day>
    {
        public bool Equals(Day x, Day y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Date == y.Date;
        }

        public int GetHashCode(Day obj)
        {
            if (obj == null)
            {
                return int.MaxValue;
            }

            var random = new Random();

            return (obj.Date.Day * obj.Date.Month * random.Next(0, 1000)) + obj.Date.Year;
        }
    }
}
