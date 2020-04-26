using NotesKeeper.Common.Enums;
using NotesKeeper.Common.EqualityComparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer
{
    public class CalendarService : ICalendarService
    {
        private readonly IEqualityComparer<DateTime> equalityComparer;

        public CalendarService()
        {
            equalityComparer = new DayEqualityComparer();
        }

        public Task<IEnumerable<DateTime>> GetDays(DateTime startDay, DateTime endDay, FrequencyEnum frequency)
        {
            return Task.Run(() => GetDaysRecursive(new List<DateTime>(), startDay, endDay, frequency).AsEnumerable());
        }

        private ICollection<DateTime> GetDaysRecursive(ICollection<DateTime> days, DateTime start, DateTime end, FrequencyEnum frequency)
        {
            if (start > end)
            {
                return days;
            }

            days.Add(start);

            switch(frequency)
            {
                case FrequencyEnum.EveryDay:
                    start = start.AddDays(1);
                    break;
                case FrequencyEnum.EveryWeek:
                    start = start.AddDays(7);
                    break;
                case FrequencyEnum.EveryMonth:
                    start = start.AddMonths(1);
                    break;
                case FrequencyEnum.EveryYear:
                    start = start.AddYears(1);
                    break;
                case FrequencyEnum.Once:
                    return days;
            }

            return GetDaysRecursive(days, start, end, frequency);
        }
    }
}
