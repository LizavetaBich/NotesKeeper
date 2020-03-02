using NotesKeeper.Common;
using NotesKeeper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer
{
    public interface ICalendarService
    {
        Task UpdateDays(IEnumerable<Day> days);

        Task<IEnumerable<Day>> UpdateDaysWithEvent(CustomEvent item, FrequencyEnum frequency);
    }
}
