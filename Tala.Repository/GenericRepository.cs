using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tala.Core.Specifications;
using Tala.Repository;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
         => await ApplySpec(spec).ToListAsync();

        public Task<T> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
         => ApplySpec(spec).FirstOrDefaultAsync();

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
         => await ApplySpec(spec).CountAsync();

        public IQueryable<T> ApplySpec(ISpecification<T> spec)
         => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
    }
}
