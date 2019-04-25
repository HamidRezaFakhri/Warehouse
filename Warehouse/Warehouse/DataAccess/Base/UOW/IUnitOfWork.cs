namespace Warehouse.DataAccess.Base.UOW
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Warehouse.DataAccess.Base.Repository;

    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>();
        TRepository GetCustomRepository<TRepository>();
    }
}