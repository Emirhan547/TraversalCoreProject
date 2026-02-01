using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T>
    {
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetListAsync();
        Task<IReadOnlyList<T>> GetListIncludingDeletedAsync();
        Task<IReadOnlyList<T>> GetDeletedListAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListByFilterAsync(Expression<Func<T, bool>> filter);

    }
}
