using AutoMapper;
using Homies.Data.Common.Repositories;
using Homies.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace Homies.Services
{
    public class TypeService : ITypeService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public TypeService(IRepository _repository, IMapper mapper)
        {
            this.repository = _repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Data.Models.Type>> GetAllTypesAsync()
        {
            return await this.repository.AllReadonly<Data.Models.Type>()
                .ToArrayAsync();
        }
    }
}
