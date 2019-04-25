namespace Warehouse.DataAccess.Base.Repository
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public abstract class RepositoryBase<TContext> : IRepositoryInjection where TContext : DbContext
    {
        protected RepositoryBase(ILogger<DataAccess> logger, TContext context)
        {
            Logger = logger;
            Context = context;
        }

        protected ILogger Logger { get; private set; }
        protected TContext Context { get; private set; }

        public IRepositoryInjection SetContext(DbContext context)
        {
            Context = (TContext)context;
            return this;
        }

        //IRepositoryInjection<TContext> IRepositoryInjection<TContext>.SetContext(TContext context)
        //{
        //    this.Context = context;
        //    return this;
        //}
    }
}