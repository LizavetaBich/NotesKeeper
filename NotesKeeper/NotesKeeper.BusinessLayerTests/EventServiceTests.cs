using Moq;
using NotesKeeper.BusinessLayer;
using NotesKeeper.Common;
using NotesKeeper.Common.Enums;
using NotesKeeper.Common.Interfaces;
using NotesKeeper.Common.Models;
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
        private readonly Mock<IConfigProvider> configProviderMock;

        public EventServiceTests()
        {
            var userConfig = new UserConfig(Guid.NewGuid(), Guid.NewGuid(), 5, 2);

            calendarServiceMock = new Mock<ICalendarService>();
            repositoryMock = new Mock<IRepository>();
            configProviderMock = new Mock<IConfigProvider>();
            configProviderMock.Setup(x => x.GetUserConfig()).Returns(Task.FromResult(userConfig));
            eventService = new EventService(calendarServiceMock.Object, repositoryMock.Object, configProviderMock.Object);
        }

        [Fact]
        public void ConstructorWithNullCalendarServiceTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EventService(null, repositoryMock.Object, configProviderMock.Object));
        }

        [Fact]
        public void ConstructorWithNullRepositoryTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EventService(calendarServiceMock.Object, null, configProviderMock.Object));
        }

        [Fact]
        public async void GetEventsByDayWithoutErrorTest()
        {
            var day = DateTime.Now;

            var item = new CustomEvent(Guid.NewGuid())
            { 
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
            repositoryMock.Verify(x => x.Read<CustomEvent>(It.IsAny<Func<CustomEvent, bool>>()), Times.Once);
        }

        [Fact]
        public async void GetEventsByStatusWithoutError()
        {
            var item = new CustomEvent(Guid.NewGuid())
            {
                Name = "some event",
                Description = "description",
                Status = StatusEnum.Free,
                Place = "some place",
                EventStartDay = DateTime.Now,
                EventLastDay = DateTime.Now.AddYears(2),
                Frequency = FrequencyEnum.EveryMonth
            };

            var item2 = new CustomEvent(Guid.NewGuid())
            {
                Name = "some event 2",
                Description = "description",
                Status = StatusEnum.Busy,
                Place = "place",
                EventStartDay = DateTime.Now,
                EventLastDay = DateTime.Now.AddYears(2),
                Frequency = FrequencyEnum.EveryMonth
            };

            var items = new List<CustomEvent>();
            items.Add(item);
            items.Add(item2);

            repositoryMock.Setup(x => x.Read<CustomEvent>(It.IsAny<Func<CustomEvent, bool>>()))
                .Returns(Task.FromResult(items.Where(x => x.Status == StatusEnum.Busy).AsEnumerable()));

            var result = await eventService.GetEventsByStatus(StatusEnum.Busy);

            Assert.Equal(item2, result.ElementAt(0));
            Assert.NotNull(result.ElementAt(0).Days);
            repositoryMock.Verify(x => x.Read<CustomEvent>(It.IsAny<Func<CustomEvent, bool>>()), Times.Once);
        }

        [Fact]
        public async void GetEventsByIdWithoutError()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            var item = new CustomEvent(guid1)
            {
                Name = "some event",
                Description = "description",
                Status = StatusEnum.Free,
                Place = "some place"
            };

            var item2 = new CustomEvent(guid2)
            {
                Name = "some event 2",
                Description = "description",
                Status = StatusEnum.Busy,
                Place = "place"
            };

            var items = new List<CustomEvent>();
            items.Add(item);
            items.Add(item2);

            repositoryMock.Setup(x => x.Read<CustomEvent>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(items.Where(x => x.Id == guid1).Single()));

            var result = await eventService.GetEventById(guid1);

            Assert.Equal(item, result);
            repositoryMock.Verify(x => x.Read<CustomEvent>(It.IsAny<Guid>()), Times.Once);
        }
    }
}
