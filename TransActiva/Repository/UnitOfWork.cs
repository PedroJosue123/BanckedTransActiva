using System.Collections;
using TransActiva.Context;
using TransActiva.Interface;

namespace TransActiva.Repository;

public class UnitOfWork: IUnitOfWork
{
    private Hashtable? _repositories{ get; }
    private readonly TransactivaDbContext _context;

    public UnitOfWork(TransactivaDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public async Task<int> SaveChange()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;
        if (_repositories.ContainsKey(type))
            return (IGenericRepository<TEntity>)_repositories[type];

        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

        if (repositoryInstance != null)
        {
            _repositories.Add(type, repositoryInstance);
            return (IGenericRepository<TEntity>)repositoryInstance;
        }

        throw new Exception($"Could not create repository instance for type {type}");
    }

}