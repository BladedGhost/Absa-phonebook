using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ABSA.PB.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task<bool> AnyAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        IEnumerable<T> Get(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetAll();

        Task<T> GetByIdAsync(Guid id);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);
    }
}