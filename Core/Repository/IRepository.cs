using System.Linq.Expressions;

namespace Core.Repository;

public interface IRepository<T> where T:class
{
    Task<T> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task AddAsync(T entity);

    IQueryable<T> Where(Expression<Func<T,bool>> predicate);

    void Delete(T entity);

    T Update(T entity);
}
