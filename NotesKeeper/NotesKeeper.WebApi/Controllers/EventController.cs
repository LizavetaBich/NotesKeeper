using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesKeeper.BusinessLayer;
using NotesKeeper.BusinessLayer.Models;
using NotesKeeper.Common;
using NotesKeeper.WebApi.ViewModels;
using NotesKeeper.WebApi.ViewModels.Events;
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
        private readonly IMapper _mapper;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventViewModel createEventViewModel)
        {
            var result = await _eventService.CreateEvent(_mapper.Map<CreateEventModel>(createEventViewModel));

            if (result == null)
            {
                return new StatusCodeResult(500);
            } else
            {
                return Ok(this._mapper.Map<CalendarEventViewModel>(result));
            }
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<CalendarEventViewModel>> GetAll([FromQuery] DateTime start, [FromQuery] DateTime end, CancellationToken token = default)
        {
            var result = await _eventService.GetAllEvents(start, end).ConfigureAwait(false);

            return this._mapper.Map<IEnumerable<CalendarEventViewModel>>(result);
        }
    }
}
