namespace TransActiva.Interface;

public interface IUnitOfWork : IDisposable 
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> SaveChange();
}