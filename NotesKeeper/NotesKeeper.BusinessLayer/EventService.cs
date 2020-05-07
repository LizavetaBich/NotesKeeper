using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotesKeeper.BusinessLayer.Models;
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
        private readonly IEqualityComparer<Day> _dayEqualityComparer;

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

        public async Task<CustomEvent> CreateEvent(CreateEventModel item)
        {
            Guard.IsNotNull(item);

            var days = await this._calendarService.CreateDays(item);
            var calendarEvent = this.ConstructEvent(item);

            await this._dbContext.Events.AddAsync(calendarEvent).ConfigureAwait(false);
            await this._dbContext.SaveChangesAsync(true).ConfigureAwait(false);

            var daysEvents = days.Select(day => new EventDay { 
                DayId = day.Id,
                EventId = calendarEvent.Id
            });

            calendarEvent.EventDays = daysEvents.ToList();
            await this._dbContext.SaveChangesAsync(true).ConfigureAwait(false);

            return calendarEvent;
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

        public Task<IEnumerable<CustomEvent>> GetAllEvents(DateTime startDay, DateTime endDay)
        {
            return Task.Run(() =>
            {
                var items = this._dbContext.Events
                    .Include(item => item.EventDays)
                    .ThenInclude(item => item.Day)
                    .Where(item => item.EventDays.Any(day => day.Day.Date >= startDay && day.Day.Date <= endDay))
                    .AsEnumerable();

                return items;
            });
        }

        public Task<CustomEvent> GetEventById(Guid id)
        {
            return Task.Run(() => {
                var item = this._dbContext.Events.Single(x => x.Id == id);

                return item;
            });
        }

        public Task<IEnumerable<CustomEvent>> GetEventsByDay(DateTime day)
        {
            return Task.Run(() => {
                var items = this._dbContext.Events.Where(x => x.EventDays.Any(d => this._dayEqualityComparer.Equals(d.Day, day)));

                return items.AsEnumerable();
            });
        }

        public Task<IEnumerable<CustomEvent>> GetEventsByStatus(StatusEnum status)
        {
            return Task.Run(() => {
                var items = this._dbContext.Events.Where(x => x.Status == status);

                return items.AsEnumerable();
            });
        }

        public Task<CustomEvent> UpdateEvent(CustomEvent item)
        {
            Guard.IsNotNull(item);

            return Task.Run(async () => {
                this._dbContext.Events.Update(item);
                await this._dbContext.SaveChangesAsync(true).ConfigureAwait(false);

                return item;
            });
        }

        private CustomEvent ConstructEvent(CreateEventModel model)
        {
            return new CustomEvent()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Name = model.Title,
                Description = model.Description,
                Place = model.Place,
                EventStartTime = new DateTime(1970, 1, 1, model.StartTime.Hours, model.StartTime.Minutes, 0, 0),
                EventLastTime = new DateTime(1970, 1, 1, model.EndTime.Hours, model.EndTime.Minutes, 0, 0),
                AllDay = model.IsAllDay,
                BackgroundColor = model.BackgroundColor
            };
        }
    }
}
