using Core.Entities;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}
