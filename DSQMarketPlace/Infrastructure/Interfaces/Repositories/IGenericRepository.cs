using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> ListAllAsync();
        Task DeleteByIdAsync(int id);
        Task AddAsync(T entity);
        Task SaveChanges();
        Task RemoveByCondition(Func<T, bool> func);
    }
}
