using Library.Data.Models;
using Library.Models.Books;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;

        public BookController(IBookService _bookService, ICategoryService _categoryService)
        {
            this.bookService = _bookService;
            this.categoryService = _categoryService;
        }

        public async Task<IActionResult> All()
        {
            var books = await this.bookService.GetAllBooksAsync();

            return this.View(books);
        }

        public async Task<IActionResult> Mine()
        {
            string userId = this.GetUserId();

            var allUserBooksViewModel = new AllUserBooksViewModel
            {
                Books = await this.bookService.GetAllUserBooksAsync(userId)
            };

            return this.View(allUserBooksViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel addBookViewModel = new AddBookViewModel
            {
                Categories = await this.categoryService.GetAllCategoriesAsync()
            };

            return this.View(addBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel addBookViewModel)
        {
            decimal rating;

            if (!decimal.TryParse(addBookViewModel.Rating, out rating) || rating < 0 || rating > 10)
            {
                ModelState.AddModelError(nameof(addBookViewModel.Rating), "Rating must be a number between 0 and 10.");

                return View(addBookViewModel);
            }

            if (!ModelState.IsValid)
            {
                return this.View(addBookViewModel);
            }

            await this.bookService.CreateBookAsync(addBookViewModel);

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Book bookToEdit = await this.bookService.GetBookByIdAsync(id);

            EditBookViewModel editBookViewModel = new EditBookViewModel
            {
                Id = id,
                Title = bookToEdit.Title,
                Author = bookToEdit.Author,
                Description = bookToEdit.Description,
                Url = bookToEdit.ImageUrl,
                Rating = bookToEdit.Rating.ToString(),
                CategoryId = bookToEdit.CategoryId,
                Categories = await this.categoryService.GetAllCategoriesAsync()
            };

            return this.View(editBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookViewModel editBookViewModel)
        {
            decimal rating;

            if (!decimal.TryParse(editBookViewModel.Rating, out rating) || rating < 0 || rating > 10)
            {
                ModelState.AddModelError(nameof(editBookViewModel.Rating), "Rating must be a number between 0 and 10.");

                editBookViewModel.Id = id;

                return this.View(editBookViewModel);
            }

            if (!ModelState.IsValid)
            {
                editBookViewModel.Id = id;

                return this.View(editBookViewModel);
            }

            await this.bookService.EditBookAsync(id, editBookViewModel);

            return this.RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            Book book = await this.bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return this.RedirectToAction(nameof(All));
            }

            string userId = this.GetUserId();

            await this.bookService.AddBookToUserAsync(userId, book);

            return this.RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            Book book = await this.bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return this.RedirectToAction(nameof(Mine));
            }

            string userId = this.GetUserId();

            await this.bookService.RemoveBookFromUserCollectionAsync(userId, book);

            return this.RedirectToAction(nameof(Mine));
        }
    }
}