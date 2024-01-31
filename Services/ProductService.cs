using Azure.Core;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(TestDbContext context) : base(context)
        {
        }
    }
}
