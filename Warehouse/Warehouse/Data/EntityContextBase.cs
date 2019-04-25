namespace Warehouse.Data
{
    using Microsoft.EntityFrameworkCore;

    public class EntityContextBase<TContext> : DbContext, IEntityContext where TContext : DbContext
    {
        public EntityContextBase(DbContextOptions<TContext> options) :
            base(options)
        { }
    }
}