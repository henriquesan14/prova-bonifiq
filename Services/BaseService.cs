using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : Entity
    {
        protected readonly TestDbContext _context;

        public BaseService(TestDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<TEntity>> GetPagedList(int page, int pageSize)
        {
            var items = await _context.Set<TEntity>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedList<TEntity>(items, items.ToList().Count(), page, pageSize);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
