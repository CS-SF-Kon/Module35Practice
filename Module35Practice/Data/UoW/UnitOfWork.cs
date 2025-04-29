using Microsoft.EntityFrameworkCore.Infrastructure;
using Module35Practice.Data.Repository;

namespace Module35Practice.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private AppContext _appContext;

    private Dictionary<Type, object> _repositories;

    public UnitOfWork(AppContext app)
    {
        this._appContext = app;
    }

    public void Dispose()
    {

    }

    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
    {
        if (_repositories == null)
        {
            _repositories = new Dictionary<Type, object>();
        }

        if (hasCustomRepository)
        {
            var customRepo = _appContext.GetService<IRepository<TEntity>>();
            if (customRepo != null)
            {
                return customRepo;
            }
        }

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<TEntity>(_appContext);
        }

        return (IRepository<TEntity>)_repositories[type];

    }
    public int SaveChanges(bool ensureAutoHistory = false)
    {
        throw new NotImplementedException();
    }
}
