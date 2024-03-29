using Core.Entities;

namespace Core.Repository;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetUsersProducts(string userId);
}
