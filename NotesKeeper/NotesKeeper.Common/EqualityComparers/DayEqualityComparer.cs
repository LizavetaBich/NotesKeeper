using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.EqualityComparers
{
    public class DayEqualityComparer : IEqualityComparer<DateTime>
    {
        public bool Equals(DateTime x, DateTime y)
        {
            return x.Date == y.Date;
        }

        public int GetHashCode(DateTime obj)
        {
            if (obj == null)
            {
                return int.MinValue;
            }

            return (obj.Day * obj.Month) + obj.Year;
        }
    }
}
