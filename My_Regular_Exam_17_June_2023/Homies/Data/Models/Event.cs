using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
    public class Event
    {
        public Event()
        {
            this.EventsParticipants = new HashSet<EventParticipant>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
        
        [ForeignKey(nameof(Organiser))]
        public string OrganiserId { get; set; } = null!;

        public IdentityUser Organiser { get; set; } = null!;

        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }

        public Models.Type Type { get; set; } = null!;

        public ICollection<EventParticipant> EventsParticipants { get; set; }
    }
}
