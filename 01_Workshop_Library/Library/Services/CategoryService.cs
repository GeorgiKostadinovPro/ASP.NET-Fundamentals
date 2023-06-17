using Library.Data.Common;
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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await this.repository.AllReadonly<Category>()
                .ToArrayAsync();
        }
    }
}
