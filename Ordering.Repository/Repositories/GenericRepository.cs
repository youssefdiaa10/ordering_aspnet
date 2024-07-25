using Microsoft.EntityFrameworkCore;
using Ordering.Core.Models;
using Ordering.Core.Repositories.Interfaces;
using Ordering.Core.Specifications;
using Ordering.Repository.Data;
using Ordering.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
         

        public async Task<IEnumerable<T>> GetAllAsync()
        {

            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>) await _context.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            }

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {

            if (typeof(T) == typeof(Product))
            {
                return await _context.Products.Where(p => p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;
            }

            return await _context.Set<T>().FindAsync(id);
        }






        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
           return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec).ToListAsync();
        }

        public Task<T?> GetWithSpecAsync(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec).CountAsync();
        }
    }
}
