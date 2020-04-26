using NotesKeeper.BusinessLayer;
using NotesKeeper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NotesKeeper.BusinessLayerTests
{
    public class CalendarServiceTests
    {
        private readonly ICalendarService _calendarService;

        public CalendarServiceTests()
        {
            _calendarService = new CalendarService();
        }

        [Theory]
        [InlineData(FrequencyEnum.EveryDay, 366)]
        [InlineData(FrequencyEnum.EveryMonth, 13)]
        [InlineData(FrequencyEnum.EveryWeek, 53)]
        [InlineData(FrequencyEnum.EveryYear, 2)]
        [InlineData(FrequencyEnum.Once, 1)]
        public async void GetDaysWithoutErrors(FrequencyEnum frequency, int resultDays)
        {
            var startDay = DateTime.Now;
            var endDay = startDay.AddYears(1);

            var items = await _calendarService.GetDays(startDay, endDay, frequency);

            Assert.NotNull(items);
            Assert.Equal(resultDays, items.Count());
        }
    }
}
