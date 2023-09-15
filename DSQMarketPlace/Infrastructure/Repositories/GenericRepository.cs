using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly StoreContext _storeContext;
        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task AddAsync(T entity)
        {
            await _storeContext.SaveChangesAsync();
            _storeContext.Set<T>().Add(entity);
            await _storeContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            T? entity = await _storeContext.Set<T>().FindAsync(id);
            if(entity == null) { throw new Exception("Nothing with this id is found"); }
            else
            {
                _storeContext.Set<T>().Remove(entity);
                await _storeContext.SaveChangesAsync();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T? entity = await _storeContext.Set<T>().FindAsync(id);
            if (entity == null) { throw new Exception("Nothing with this id is found"); }
            return entity;
        }

        public IQueryable<T> ListAllAsync()
        {
            return _storeContext.Set<T>();
        }
        public async Task RemoveByCondition(Func<T, bool> func)
        {
            var entities = _storeContext.Set<T>().Where(func);
            _storeContext.Set<T>().RemoveRange(entities);
            await _storeContext.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _storeContext.SaveChangesAsync();
        }
    }
}
