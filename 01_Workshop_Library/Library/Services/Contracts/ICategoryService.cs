﻿using Library.Data.Models;

namespace Library.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
