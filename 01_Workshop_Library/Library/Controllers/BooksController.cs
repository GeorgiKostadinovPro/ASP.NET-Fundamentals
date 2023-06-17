using Library.Models.Books;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;

        public BooksController(IBookService _bookService, ICategoryService _categoryService)
        {
            this.bookService = _bookService;
            this.categoryService = _categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            BookInputModel model = new BookInputModel
            {
                Categories = await this.categoryService.GetAllAsync()
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.bookService.CreateBookAsync(model);

                return this.RedirectToAction("All", "Books");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Invalid Movie!");

                return this.View(model);
            }
        }

        public async Task<IActionResult> AddToCollection(int bookId)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await this.bookService.AddBookToUserAsync(userId, bookId);
            }
            catch (Exception)
            {
                throw;
            }

            return this.RedirectToAction("All", "Books");
        }

        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            try
            {
                string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await this.bookService.RemoveAddedBookFromUserCollectionAsync(userId, bookId);
            }
            catch (Exception)
            {
                throw;
            }

            return this.RedirectToAction("Mine", "Books");
        }

        public async Task<IActionResult> Mine()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<BookViewModel> model = await this.bookService.GetUserBooksAsync(userId);

            return this.View(model);
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<BookViewModel> model = await this.bookService.GetAllAsync();

            return this.View(model);
        }
    }
}
