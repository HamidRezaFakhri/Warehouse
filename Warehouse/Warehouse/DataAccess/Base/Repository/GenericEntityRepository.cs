namespace Warehouse.DataAccess.Base.Repository
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Warehouse.Entities;

    public class GenericEntityRepository<TEntity> :
        EntityRepositoryBase<DbContext, TEntity> where TEntity : EntityBase, new()
    {
        public GenericEntityRepository(ILogger<DataAccess> logger) :
            base(logger, null){ }
    }
}