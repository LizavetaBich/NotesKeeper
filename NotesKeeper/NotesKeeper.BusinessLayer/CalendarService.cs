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
    public class CalendarService : ICalendarService
    {
        private readonly IUserDbContext _userDb;
        private readonly IConfigurationSection _userConfiguration;
        private readonly IEqualityComparer<Day> _equalityComparer;

        public CalendarService(IUserDbContext userDb, IConfiguration configuration)
        {
            _userDb = userDb;
            _userConfiguration = configuration.GetSection("UserConfig");
            _equalityComparer = new DayEqualityComparer();
        }

        public async Task<ICollection<Day>> CreateDays(CreateEventModel model)
        {
            if (model == null)
            {
                return null;
            }

            var startDay = this._userDb.Days.FirstOrDefault(item => item.Date == model.StartDate)
                ?? model.StartDate;
            var endDay = DateTime.Now.AddYears(_userConfiguration.GetValue<int>("YearsForward"));
            var days = new List<Day>();

            switch ((FrequencyEnum)model.Frequency)
            {
                case FrequencyEnum.EveryDay:
                    ConstructDays(days, startDay, endDay, date => date.AddDays(1));
                    break;
                case FrequencyEnum.EveryWeek:
                    ConstructDays(days, startDay, endDay, date => date.AddDays(7));
                    break;
                case FrequencyEnum.EveryMonth:
                    ConstructDays(days, startDay, endDay, date => date.AddMonths(1));
                    break;
                case FrequencyEnum.EveryYear:
                    ConstructDays(days, startDay, endDay, date => date.AddYears(1));
                    break;
                case FrequencyEnum.Once:
                    days.Add(startDay);
                    break;
                case FrequencyEnum.Custom:
                    CustomDays(days, endDay, model);
                    break;
                default:
                    return days;
            }

            var daysToStore = days.Except(this._userDb.Days, this._equalityComparer).ToList();
            this._userDb.Days.AddRange(daysToStore);
            await this._userDb.SaveChangesAsync(true);
            return days;
        }

        private void CustomDays(ICollection<Day> days, Day endDay, CreateEventModel model)
        {
            var start = model.StartDate;
            var end = model.EndDate ?? endDay;
            Func<DateTime, DateTime> incrementFunc = date =>
            {
                if (model.Days != null && model.Days.Any())
                {
                    var next = model.Days.Where(item => item > (int)date.DayOfWeek).Min();
                    var delta = next - (int)date.DayOfWeek;
                    return date.AddDays(delta);
                } else
                {
                    return date.AddDays(1);
                }
            };

            ConstructDays(days, start, end, incrementFunc);
        }

        private void ConstructDays(ICollection<Day> days, Day current, Day endDay, Func<DateTime, DateTime> incrementFunc)
        {
            if (current == null || current.Date.Date > endDay.Date.Date)
            {
                return;
            }

            var currentDay = this._userDb.Days.FirstOrDefault(item => item.Date.Date == current.Date.Date)
                    ?? current;

            days.Add(currentDay);
            ConstructDays(days, incrementFunc(current.Date), endDay, incrementFunc);
        }
    }
}
