using NotesKeeper.BusinessLayer.Models;
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
        Task<ICollection<Day>> CreateDays(CreateEventModel model);
    }
}
