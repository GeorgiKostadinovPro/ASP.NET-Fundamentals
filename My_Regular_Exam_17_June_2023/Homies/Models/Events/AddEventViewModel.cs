using Homies.Data.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Homies.Models.Events
{
    public class AddEventViewModel
    { 
        [Required]
        [StringLength(ValidationalConstants.EventConstants.EventNameMaxLength,
          MinimumLength = ValidationalConstants.EventConstants.EventNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(ValidationalConstants.EventConstants.EventDescriptionMaxLength,
          MinimumLength = ValidationalConstants.EventConstants.EventDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public int TypeId { get; set; }

        public IEnumerable<Data.Models.Type> Types { get; set; } = new HashSet<Data.Models.Type>();
    }
}
