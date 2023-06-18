using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Models
{
    public class IdentityUserBook
    {
        // 1. For better performance
        // 2. In order to use the repositories from Stamo
        //    because they can't work with composite key
        /*[Key]
        public int Id { get; set; }*/

        [ForeignKey(nameof(Collector))]
        public string CollectorId { get; set; } = null!;

        public IdentityUser Collector { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }

        public Book Book { get; set; } = null!;
    }
}
