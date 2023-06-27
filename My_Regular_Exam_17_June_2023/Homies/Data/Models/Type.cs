﻿using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
    public class Type
    {
        public Type()
        {
            this.Events = new HashSet<Event>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Event> Events { get; set; }
    }
}
