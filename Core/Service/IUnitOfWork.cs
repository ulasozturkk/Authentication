namespace Core.Service;

public interface IUnitOfWork
{
    Task CommitAsync();

    void Commit();
}
