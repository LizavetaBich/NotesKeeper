using System;
using System.Collections.Generic;
using System.Text;
using NotesKeeper.Common;
using NotesKeeper.Common.Enums;
using NotesKeeper.DataAccess.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using NotesKeeper.Common.EqualityComparers;

namespace NotesKeeper.BusinessLayer
{
    public class EventService : IEventService
    {
        private readonly ICalendarService _calendarService;
        private readonly IRepository _repository;
        private readonly IEqualityComparer<Day> _dayEqualityComparer;

        public EventService(ICalendarService calendarService, IRepository repository)
        {
            this._calendarService = calendarService;
            this._repository = repository;
            this._dayEqualityComparer = new DayEqualityComparer();
        }

        public async Task<CustomEvent> CreateEvent(CustomEvent item, FrequencyEnum frequency)
        {
            if (item == null)
            {
                throw new ArgumentNullException("");
            }

            var days = await _calendarService.UpdateDaysWithEvent(item, frequency);

            item.Days.ToList().AddRange(days);

            await this._repository.Create<CustomEvent>(item);

            return item;
        }

        public Task DeleteEvent(Guid id)
        {
            return this._repository.Delete<CustomEvent>(id);
        }

        public Task<IEnumerable<CustomEvent>> GetAllEvents(Day day)
        {
            if (day == null)
            {
                throw new ArgumentNullException("");
            }

            return this._repository.ReadAll<CustomEvent>();
        }

        public Task<CustomEvent> GetEventById(Guid id)
        {
            return this._repository.Read<CustomEvent>(id);
        }

        public Task<IEnumerable<CustomEvent>> GetEventsByDay(Day day)
        {
            if (day == null)
            {
                throw new ArgumentNullException("");
            }

            return this._repository.Read<CustomEvent>(x => x.Days.Any(d => this._dayEqualityComparer.Equals(d, day)));
        }

        public Task<IEnumerable<CustomEvent>> GetEventsByStatus(StatusEnum status)
        {
            return this._repository.Read<CustomEvent>(x => x.Status == status);
        }

        public Task<CustomEvent> UpdateEvent(CustomEvent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("");
            }


        }
    }
}
