using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tala.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Tala.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }


        public async Task<int> Complete()
        => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await _context.DisposeAsync();

        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories is null)
                _repositories = new Hashtable();
            var type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
