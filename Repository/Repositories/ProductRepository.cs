using Core.Entities;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<Product>> GetUsersProducts(string userId)
    {
        return _context.products.Where(p=> p.OwnerId == userId).ToListAsync();
    }
}
