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
            return await _eventService.GetAllEvents(DateTime.Now).ConfigureAwait(false);
        }
    }
}
