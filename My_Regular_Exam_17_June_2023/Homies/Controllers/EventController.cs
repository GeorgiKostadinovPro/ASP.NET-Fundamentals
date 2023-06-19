using Homies.Data.Models;
using Homies.Models.Events;
using Homies.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;
        private readonly ITypeService typeService;

        public EventController(IEventService _eventService, ITypeService _typeService)
        {
            this.eventService = _eventService;
            this.typeService = _typeService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var events = await this.eventService.GetAllEventsAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = this.GetUserId();

            var joinedEvents = await this.eventService.GetAllUserJoinedEventsAsync(userId);

            return View(joinedEvents);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            EventDetailsViewModel eventDetailsViewModel = await this.eventService.GetDetailsForEventAsync(id);

            if (eventDetailsViewModel == null)
            {
                return this.RedirectToAction(nameof(All));
            }

            return this.View(eventDetailsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel
            {
                Types = await this.typeService.GetAllTypesAsync()
            };

            return this.View(addEventViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel addEventViewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(addEventViewModel);
            }

            string userId = this.GetUserId();

            await this.eventService.CreateEventAsync(userId, addEventViewModel);

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Event eventToEdit = await this.eventService.GetEventByIdAsync(id);

            EditEventViewModel editEventViewModel = new EditEventViewModel
            {
                Id = id,
                Name = eventToEdit.Name,
                Description = eventToEdit.Description,
                Start = eventToEdit.Start.ToString("dd/MM/yyyy H:mm"),
                End = eventToEdit.End.ToString("dd/MM/yyyy H:mm"),
                TypeId = eventToEdit.TypeId,
                Types = await this.typeService.GetAllTypesAsync()
            };

            return this.View(editEventViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventViewModel editEventViewModel)
        {
            if (!ModelState.IsValid)
            {
                editEventViewModel.Id = id;
                return this.View(editEventViewModel);
            }

            await this.eventService.EditEventAsync(id, editEventViewModel);

            return this.RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            Event eventToAdd = await this.eventService.GetEventByIdAsync(id);

            if (eventToAdd == null)
            {
                return this.RedirectToAction(nameof(All));
            }

            string userId = this.GetUserId();

            await this.eventService.AddEventToUserAsync(userId, eventToAdd);

            return this.RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            Event eventToLeave = await this.eventService.GetEventByIdAsync(id);

            if (eventToLeave == null)
            {
                return this.RedirectToAction(nameof(Joined));
            }

            string userId = this.GetUserId();

            await this.eventService.RemoveEventFromUserAsync(userId, eventToLeave);

            return this.RedirectToAction(nameof(All));
        }
    }
}