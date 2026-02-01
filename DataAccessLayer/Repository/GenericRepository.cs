using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
        }

        public void DeleteAsync(T entity)
        {
            _context.Entry(entity).Property(nameof(BaseEntity.IsDeleted)).CurrentValue = true;
            _context.Set<T>().Update(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetListAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetListByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public async Task<IReadOnlyList<T>> GetListIncludingDeletedAsync()
        {
            return await _context.Set<T>().IgnoreQueryFilters().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetDeletedListAsync()
        {
            if (_context.Model.FindEntityType(typeof(T))?.FindProperty(nameof(BaseEntity.IsDeleted)) is null)
            {
                return Array.Empty<T>();
            }

            return await _context.Set<T>()
                .IgnoreQueryFilters()
                .Where(entity => EF.Property<bool>(entity, nameof(BaseEntity.IsDeleted)))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
