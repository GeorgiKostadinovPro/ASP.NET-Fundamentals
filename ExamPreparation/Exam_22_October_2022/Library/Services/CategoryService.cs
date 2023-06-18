using Library.Data.Common.Repositories;
using Library.Data.Models;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;

        public CategoryService(IRepository _repository)
        {
            this.repository = _repository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await this.repository.AllReadonly<Category>()
                .ToArrayAsync();
        }
    }
}
