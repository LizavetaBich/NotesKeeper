using Microsoft.Extensions.Configuration;
using NotesKeeper.Common;
using NotesKeeper.Common.Enums;
using NotesKeeper.Common.EqualityComparers;
using NotesKeeper.Common.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer
{
    public class EventService : IEventService
    {
        private readonly IConfiguration _configuration;
        private readonly ICalendarService _calendarService;
        private readonly IUserDbContext _dbContext;
        private readonly IEqualityComparer<DateTime> _dayEqualityComparer;

        public EventService(ICalendarService calendarService, IUserDbContext dbContext, IConfiguration configuration)
        {
            Guard.IsNotNull(calendarService);
            Guard.IsNotNull(dbContext);
            Guard.IsNotNull(configuration);

            this._calendarService = calendarService;
            this._dbContext = dbContext;
            this._configuration = configuration;
            this._dayEqualityComparer = new DayEqualityComparer();
        }

        public async Task<CustomEvent> CreateEvent(CustomEvent item)
        {
            Guard.IsNotNull(item);

            await FillDays(item);
            await this._dbContext.Events.AddAsync(item).ConfigureAwait(false);

            await this._dbContext.SaveChangesAsync(true).ConfigureAwait(false);

            return item;
        }

        public Task DeleteEvent(Guid id)
        {
            return Task.Run(async () =>
            {
                var eventToDelete = this._dbContext.Events.Single(item => item.Id == id);
                this._dbContext.Events.Remove(eventToDelete);
                await this._dbContext.SaveChangesAsync(true).ConfigureAwait(false);
            });
        }

        public Task<IEnumerable<CustomEvent>> GetAllEvents(DateTime day)
        {
            return Task.Run(async () =>
            {
                var items = this._dbContext.Events.AsEnumerable();

                foreach (var item in items)
                {
                    await FillDays(item);
                }

                return items;
            });
        }

        public Task<CustomEvent> GetEventById(Guid id)
        {
            return Task.Run(async () => {
                var item = this._dbContext.Events.Single(x => x.Id == id);

                await FillDays(item);

                return item;
            });
        }

        public Task<IEnumerable<CustomEvent>> GetEventsByDay(DateTime day)
        {
            return Task.Run(() => {
                var items = this._dbContext.Events.Where(x => x.Days.Any(d => this._dayEqualityComparer.Equals(d, day)));

                Parallel.ForEach(items, async item => await FillDays(item));

                return items.AsEnumerable();
            });
        }

        public Task<IEnumerable<CustomEvent>> GetEventsByStatus(StatusEnum status)
        {
            return Task.Run(() => {
                var items = this._dbContext.Events.Where(x => x.Status == status);

                Parallel.ForEach(items, async item => await FillDays(item));

                return items.AsEnumerable();
            });
        }

        public Task<CustomEvent> UpdateEvent(CustomEvent item)
        {
            Guard.IsNotNull(item);

            return Task.Run(async () => {
                await FillDays(item);

                this._dbContext.Events.Update(item);
                await this._dbContext.SaveChangesAsync(true).ConfigureAwait(false);

                return item;
            });
        }

        private async Task FillDays(CustomEvent item)
        {
            item.Days.Clear();

            var userConfig = this._configuration.GetSection("UserConfig");
            var lastDay = item.EventLastDay.HasValue ? item.EventLastDay.Value : DateTime.Now.AddYears(userConfig.GetValue<int>("YearsForward"));

            foreach (var day in await this._calendarService.GetDays(item.EventStartDay, lastDay, item.Frequency).ConfigureAwait(false))
            {
                item.Days.Add(day);
            }
        }
    }
}
