using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        } 
        
        // View the Configuration folder to see 
        // the validational attributes and constants

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; }
    }
}
