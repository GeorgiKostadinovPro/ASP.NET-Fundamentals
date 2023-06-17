using Library.Data;
using Library.Data.Common;
using Library.Data.Models;
using Library.Models.Books;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NuGet.LibraryModel;
using System.Linq;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository repository;
        //private readonly LibraryDbContext dbContext;

        public BookService(IRepository _repository)
        {
            this.repository = _repository;
        }

        public async Task CreateBookAsync(BookInputModel model)
        {
            Book book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
            };

            await this.repository.AddAsync(book);
            await this.repository.SaveChangesAsync();
        }
        
        public async Task AddBookToUserAsync(string userId, int bookId)
        {
            ApplicationUser user = await this.repository.All<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("User does NOT exist!");
            }

            ApplicationUserBook userBook = await this.repository.All<ApplicationUserBook>()
                .FirstOrDefaultAsync(aub => aub.ApplicationUserId == userId && aub.BookId == bookId);

            if (userBook != null)
            {
                if (userBook.IsActive == false)
                {
                    userBook.IsActive = true;
                }
            }
            else
            {
                user.ApplicationUsersBooks.Add(new ApplicationUserBook
                {
                   ApplicationUserId = userId,
                   BookId = bookId,
                   IsActive = true
                });
            }

            await this.repository.SaveChangesAsync();
        }
        
        public async Task RemoveAddedBookFromUserCollectionAsync(string userId, int bookId)
        {
            ApplicationUser user = await this.repository.AllReadonly<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("User does NOT exist!");
            }

            ApplicationUserBook userBook = await this.repository.All<ApplicationUserBook>()
                .FirstOrDefaultAsync(aub => aub.ApplicationUserId == userId && aub.BookId == bookId);

            userBook.IsActive = false;

            await this.repository.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<BookViewModel>> GetUserBooksAsync(string userId)
        {
            return await this.repository.AllReadonly<ApplicationUserBook>()
                .Where(aub => aub.ApplicationUserId == userId && aub.IsActive == true)
                .Select(aub => new BookViewModel
                {
                    Id = aub.Book.Id,
                    Title = aub.Book.Title,
                    Author = aub.Book.Author,
                    Description = aub.Book.Description,
                    ImageUrl = aub.Book.ImageUrl,
                    Rating = aub.Book.Rating.ToString(),
                    Category = aub.Book.Category.Name
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            return await this.repository.AllReadonly<Book>()
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating.ToString(),
                    Category = b.Category.Name
                })
                .ToArrayAsync();
        }
    }
}