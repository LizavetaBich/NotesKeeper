using NotesKeeper.Common;
using NotesKeeper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer
{
    public interface IEventService
    {
        Task<IEnumerable<CustomEvent>> GetEventsByDay(Day day);

        Task<IEnumerable<CustomEvent>> GetAllEvents(Day day);

        Task<IEnumerable<CustomEvent>> GetEventsByStatus(StatusEnum status);

        Task<CustomEvent> CreateEvent(CustomEvent item, FrequencyEnum frequency);

        Task<CustomEvent> GetEventById(Guid id);

        Task DeleteEvent(Guid id);

        Task<CustomEvent> UpdateEvent(CustomEvent item);
    }
}
