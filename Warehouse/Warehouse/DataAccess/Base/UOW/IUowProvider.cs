namespace Warehouse.DataAccess.Base.UOW
{
    using Microsoft.EntityFrameworkCore;

    public interface IUowProvider
    {
        IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false);
        IUnitOfWork CreateUnitOfWork<TEntityContext>(bool trackChanges = true, bool enableLogging = false) where TEntityContext : DbContext;
    }
}