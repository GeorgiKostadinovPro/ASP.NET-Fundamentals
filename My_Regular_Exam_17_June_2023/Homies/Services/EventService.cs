using AutoMapper;
using AutoMapper.QueryableExtensions;
using Homies.Data.Common.Repositories;
using Homies.Data.Models;
using Homies.Models.Events;
using Homies.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public EventService(IRepository _repository, IMapper mapper)
        {
            this.repository = _repository;
            this.mapper = mapper;
        }

        public async Task CreateEventAsync(string userId, AddEventViewModel addEventViewModel)
        {
            Event eventToCreate = this.mapper.Map<Event>(addEventViewModel);

            eventToCreate.OrganiserId = userId;
            eventToCreate.CreatedOn = DateTime.UtcNow;

            await this.repository.AddAsync(eventToCreate);
            await this.repository.SaveChangesAsync();
        }

        public async Task EditEventAsync(int eventId, EditEventViewModel editEventViewModel)
        {
            Event eventToEdit = await this.GetEventByIdAsync(eventId);

            if (eventToEdit != null)
            {
                if (DateTime.TryParseExact(editEventViewModel.Start, "dd/MM/yyyy H:mm", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startResult)
                    && DateTime.TryParseExact(editEventViewModel.End, "dd/MM/yyyy H:mm", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endResult))
                {
                    eventToEdit.Name = editEventViewModel.Name;
                    eventToEdit.Description = editEventViewModel.Description;
                    eventToEdit.Start = startResult;
                    eventToEdit.End = endResult;
                    eventToEdit.TypeId = editEventViewModel.TypeId;

                    this.repository.Update(eventToEdit);

                    await this.repository.SaveChangesAsync();
                }
            }
        }

        public async Task AddEventToUserAsync(string userId, Event eventToAdd)
        {
            var eventParticipant = await this.repository.All<EventParticipant>()
                .FirstOrDefaultAsync(ep => ep.HelperId == userId && ep.EventId == eventToAdd.Id);

            if (eventParticipant == null)
            {
                eventParticipant = new EventParticipant
                {
                    HelperId = userId,
                    EventId = eventToAdd.Id
                };

                await this.repository.AddAsync(eventParticipant);
                await this.repository.SaveChangesAsync();
            }
        }

        public async Task RemoveEventFromUserAsync(string userId, Event eventToLeave)
        {
            var eventParticipant = await this.repository.All<EventParticipant>()
                .FirstOrDefaultAsync(ep => ep.HelperId == userId && ep.EventId == eventToLeave.Id);

            if (eventParticipant != null)
            {
                this.repository.Delete(eventParticipant);

                await this.repository.SaveChangesAsync();
            }
        }

        public async Task<EventDetailsViewModel> GetDetailsForEventAsync(int eventId)
        {
            Event eventToDisplay = await this.repository.All<Event>()
                .Include(e => e.Organiser)
                .Include(e => e.Type)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventToDisplay == null)
            {
                return null;
            } 
            
            EventDetailsViewModel eventDetailsViewModel 
                    = this.mapper.Map<EventDetailsViewModel>(eventToDisplay);

            eventDetailsViewModel.Organiser = eventToDisplay.Organiser.Email;

            return eventDetailsViewModel;
        } 
        
        public async Task<IEnumerable<EventViewModel>> GetAllEventsAsync()
        {
            return await this.repository.AllReadonly<Event>()
                .ProjectTo<EventViewModel>(this.mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
        
        public async Task<IEnumerable<EventViewModel>> GetAllUserJoinedEventsAsync(string userId)
        {
            return await this.repository.All<EventParticipant>()
                .Where(ep => ep.HelperId == userId)
                .Include(ep => ep.Event)
                .Select(ep => ep.Event)
                .ProjectTo<EventViewModel>(this.mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await this.repository.All<Event>()
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }
    }
}