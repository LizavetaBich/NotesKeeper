using Moq;
using NotesKeeper.BusinessLayer;
using NotesKeeper.Common;
using NotesKeeper.Common.Enums;
using NotesKeeper.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NotesKeeper.BusinessLayerTests
{
    public class EventServiceTests
    {
        private readonly IEventService eventService;
        private readonly Mock<ICalendarService> calendarServiceMock;
        private readonly Mock<IRepository> repositoryMock;

        public EventServiceTests()
        {
            calendarServiceMock = new Mock<ICalendarService>();
            repositoryMock = new Mock<IRepository>();
            eventService = new EventService(calendarServiceMock.Object, repositoryMock.Object);
        }

        [Fact]
        public void ConstructorWithNullCalendarServiceTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EventService(null, repositoryMock.Object));
        }

        [Fact]
        public void ConstructorWithNullRepositoryTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EventService(calendarServiceMock.Object, null));
        }

        [Fact]
        public async void GetEventsByDayWithoutErrorTest()
        {
            Day day = DateTime.Now;

            var item = new CustomEvent { 
                Name = "some event",
                Description = "description",
                Status = StatusEnum.Free,
                Place = "some place"
            };

            var items = new List<CustomEvent>();
            items.Add(item);

            item.Days.ToList().Add(day);

            repositoryMock.Setup(x => x.Read<CustomEvent>(It.IsAny<Func<CustomEvent, bool>>()))
                .Returns(Task.FromResult(items.AsEnumerable()));

            var result = await eventService.GetEventsByDay(day);

            Assert.Equal(items, result);
        }

        [Fact]
        public void GetEventsByDayWithError()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => eventService.GetEventsByDay(null));
        }
    }
}
