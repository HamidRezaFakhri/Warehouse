namespace Warehouse.DataAccess.Base.Repository
{
    using Microsoft.EntityFrameworkCore;

    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}