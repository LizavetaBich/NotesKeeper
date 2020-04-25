using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesKeeper.BusinessLayer;
using NotesKeeper.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NotesKeeper.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<CustomEvent>> GetAll(CancellationToken token = default)
        {
            await _eventService.CreateEvent(new CustomEvent() { 
                Id = Guid.NewGuid(),
                Frequency = Common.Enums.FrequencyEnum.Once,
                Name = "name",
                Description = "descr",
                Status = Common.Enums.StatusEnum.Free,
                EventStartDay = DateTime.Now,
                Place = "place"
            }).ConfigureAwait(false);
            return await _eventService.GetAllEvents(DateTime.Now).ConfigureAwait(false);
        }
    }
}
