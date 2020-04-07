using System;
using System.Collections.Generic;
using System.Text;
using NotesKeeper.Common;
using NotesKeeper.Common.Enums;
using NotesKeeper.DataAccess.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using NotesKeeper.Common.EqualityComparers;
using NotesKeeper.Common.Interfaces;

namespace NotesKeeper.BusinessLayer
{
    public class EventService : IEventService
    {
        private readonly IConfigProvider _configProvider;
        private readonly ICalendarService _calendarService;
        private readonly IRepository _repository;
        private readonly IEqualityComparer<DateTime> _dayEqualityComparer;

        public EventService(ICalendarService calendarService, IRepository repository, IConfigProvider configProvider)
        {
            Guard.IsNotNull(calendarService);
            Guard.IsNotNull(repository);
            Guard.IsNotNull(configProvider);

            this._calendarService = calendarService;
            this._repository = repository;
            this._configProvider = configProvider;
            this._dayEqualityComparer = new DayEqualityComparer();
        }

        public async Task<CustomEvent> CreateEvent(CustomEvent item, FrequencyEnum frequency)
        {
            Guard.IsNotNull(item);

            await this._repository.Create<CustomEvent>(item).ConfigureAwait(false);

            await FillDays(item);

            return item;
        }

        public Task DeleteEvent(Guid id)
        {
            return this._repository.Delete<CustomEvent>(id);
        }

        public async Task<IEnumerable<CustomEvent>> GetAllEvents(DateTime day)
        {
            var items = await this._repository.ReadAll<CustomEvent>().ConfigureAwait(false);

            foreach (var item in items)
            {
                await FillDays(item);
            }

            return items;
        }

        public async Task<CustomEvent> GetEventById(Guid id)
        {
            var item = await this._repository.Read<CustomEvent>(id).ConfigureAwait(false);

            await FillDays(item);

            return item;
        }

        public async Task<IEnumerable<CustomEvent>> GetEventsByDay(DateTime day)
        {
            var items = await this._repository.Read<CustomEvent>(x => x.Days.Any(d => this._dayEqualityComparer.Equals(d, day)))
                .ConfigureAwait(false);

            foreach (var item in items)
            {
                await FillDays(item);
            }

            return items;
        }

        public async Task<IEnumerable<CustomEvent>> GetEventsByStatus(StatusEnum status)
        {
            var items = await this._repository.Read<CustomEvent>(x => x.Status == status)
                .ConfigureAwait(false);
            
            foreach (var item in items)
            {
                await FillDays(item);
            }

            return items;
        }

        public async Task<CustomEvent> UpdateEvent(CustomEvent item)
        {
            Guard.IsNotNull(item);

            await FillDays(item);

            return await this._repository.Update<CustomEvent>(item).ConfigureAwait(false);
        }

        private async Task FillDays(CustomEvent item)
        {
            item.Days.Clear();

            var userConfig = await this._configProvider.GetUserConfig();
            var lastDay = item.EventLastDay.HasValue ? item.EventLastDay.Value : DateTime.Now.AddYears(userConfig.YearsForward);

            foreach (var day in await this._calendarService.GetDays(item.EventStartDay, lastDay, item.Frequency).ConfigureAwait(false))
            {
                item.Days.Add(day);
            }
        }
    }
}
