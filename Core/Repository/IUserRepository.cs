using Core.Entities;

namespace Core.Repository;

public interface IUserRepository : IRepository<User>
{
    Task<List<User>> GetUsers();
}
