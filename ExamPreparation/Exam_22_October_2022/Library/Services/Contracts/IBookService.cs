using Library.Data.Models;
using Library.Models.Books;

namespace Library.Services.Contracts
{
    public interface IBookService
    {
        Task CreateBookAsync(AddBookViewModel addBookViewModel);

        Task EditBookAsync(int id, EditBookViewModel editBookViewModel);

        Task AddBookToUserAsync(string userId, Book book);

        Task RemoveBookFromUserCollectionAsync(string userId, Book book);

        Task<Book> GetBookByIdAsync(int bookId);

        Task<IEnumerable<BookViewModel>> GetAllBooksAsync();

        Task<IEnumerable<MyBookViewModel>> GetAllUserBooksAsync(string userId);
    }
}
