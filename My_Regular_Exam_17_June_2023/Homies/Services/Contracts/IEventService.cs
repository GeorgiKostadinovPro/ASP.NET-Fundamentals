using Homies.Data.Models;
using Homies.Models.Events;

namespace Homies.Services.Contracts
{
    public interface IEventService
    {
        Task CreateEventAsync(string userId, AddEventViewModel addEventViewModel);

        Task EditEventAsync(int eventId, EditEventViewModel editEventViewModel);
        Task AddEventToUserAsync(string userId, Event eventToAdd);

        Task RemoveEventFromUserAsync(string userId, Event eventToLeave);

        Task<EventDetailsViewModel> GetDetailsForEventAsync(int eventId);

        Task<Event> GetEventByIdAsync(int eventId);

        Task<IEnumerable<EventViewModel>> GetAllEventsAsync();

        Task<IEnumerable<EventViewModel>> GetAllUserJoinedEventsAsync(string userId);
    }
}
