using ProvaPub.Models;

namespace ProvaPub.Services
{
    public interface IBaseService<TEntity> : IDisposable where TEntity : Entity
    {
        Task<PagedList<TEntity>> GetPagedList(int page, int pageSize);
    }
}
