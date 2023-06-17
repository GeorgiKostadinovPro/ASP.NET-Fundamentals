using Library.Models.Books;

namespace Library.Services.Contracts
{
    public interface IBookService
    {
        Task CreateBookAsync(BookInputModel model);

        Task AddBookToUserAsync(string userId, int bookId);

        Task RemoveAddedBookFromUserCollectionAsync(string userId, int bookId);

        Task<IEnumerable<BookViewModel>> GetUserBooksAsync(string userId);

        Task<IEnumerable<BookViewModel>> GetAllAsync();
    }
}
