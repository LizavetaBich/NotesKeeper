using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.EqualityComparers
{
    public class DayEqualityComparer : IEqualityComparer<Day>
    {
        public bool Equals(Day x, Day y)
        {
            return x == y;
        }

        public int GetHashCode(Day obj)
        {
            if (obj == null)
            {
                return int.MinValue;
            }

            return (obj.DayNumber * obj.Month) + obj.Year;
        }
    }
}
