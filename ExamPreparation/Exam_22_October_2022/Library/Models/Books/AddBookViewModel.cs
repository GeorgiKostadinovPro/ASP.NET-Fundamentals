using Library.Data.Common.Constants;
using Library.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Books
{
    public class AddBookViewModel
    {
        [Required]
        [StringLength(ValidationalConstants.BookConstants.BookTitleMaxLength,
            MinimumLength = ValidationalConstants.BookConstants.BookTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ValidationalConstants.BookConstants.BookAuthorMaxLength,
            MinimumLength = ValidationalConstants.BookConstants.BookAuthorMinLength)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(ValidationalConstants.BookConstants.BookDescriptionMaxLength,
            MinimumLength = ValidationalConstants.BookConstants.BookDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Url { get; set; } = null!;

        [Required]
        public string Rating { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new HashSet<Category>();
    }
}
