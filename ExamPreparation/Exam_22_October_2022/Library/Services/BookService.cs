using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Data.Common.Repositories;
using Library.Data.Models;
using Library.Models.Books;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public BookService(IRepository _repository, IMapper _mapper)
        {
            this.repository = _repository;
            this.mapper = _mapper;
        }

        public async Task CreateBookAsync(AddBookViewModel addBookViewModel)
        {
            Book book = this.mapper.Map<Book>(addBookViewModel);

            if (decimal.TryParse(addBookViewModel.Rating, out decimal result) == true)
            {
                book.Rating = result;

                await this.repository.AddAsync(book);
                await this.repository.SaveChangesAsync();
            }
        }

        public async Task EditBookAsync(int id, EditBookViewModel editBookViewModel)
        {
            Book book = await this.GetBookByIdAsync(id);

            if (book != null)
            {
                if (decimal.TryParse(editBookViewModel.Rating, out decimal result) == true)
                {
                    book.Title = editBookViewModel.Title;
                    book.Author = editBookViewModel.Author;
                    book.Description = editBookViewModel.Description;
                    book.ImageUrl = editBookViewModel.Url;
                    book.Rating = result;
                    book.CategoryId = editBookViewModel.CategoryId;

                    this.repository.Update(book);
                    await this.repository.SaveChangesAsync();
                }
            }
        }

        public async Task AddBookToUserAsync(string userId, Book book)
        {
            var identityUserBook = await this.repository.All<IdentityUserBook>()
                .FirstOrDefaultAsync(iub => iub.CollectorId == userId && iub.BookId == book.Id);

            if (identityUserBook == null)
            {
                identityUserBook = new IdentityUserBook
                {
                    CollectorId = userId,
                    BookId = book.Id
                };

                await this.repository.AddAsync(identityUserBook);
                await this.repository.SaveChangesAsync();
            }
        }

        public async Task RemoveBookFromUserCollectionAsync(string userId, Book book)
        {
            var identityUserBook = await this.repository.All<IdentityUserBook>()
               .FirstOrDefaultAsync(iub => iub.CollectorId == userId && iub.BookId == book.Id);

            if (identityUserBook != null)
            {
                this.repository.Delete(identityUserBook);

                await this.repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {
            return await this.repository.AllReadonly<Book>()
                .ProjectTo<BookViewModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<MyBookViewModel>> GetAllUserBooksAsync(string userId)
        {
            return await this.repository.All<IdentityUserBook>()
                .Where(iub => iub.CollectorId == userId)
                .Include(iub => iub.Book)
                .Select(iub => iub.Book)
                .ProjectTo<MyBookViewModel>(mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await this.repository.All<Book>()
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }
    }
}
