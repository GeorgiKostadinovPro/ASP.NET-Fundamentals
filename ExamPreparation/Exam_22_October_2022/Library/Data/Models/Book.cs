using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class Book
    {
        public Book()
        {
            this.UsersBooks = new HashSet<IdentityUserBook>();
        }

        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<IdentityUserBook> UsersBooks { get; set; }
    }
}
