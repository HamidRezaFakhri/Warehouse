namespace Warehouse.DataAccess.Base.UOW
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class UnitOfWork : UnitOfWorkBase<DbContext>, IUnitOfWork
    {
        public UnitOfWork(DbContext context, IServiceProvider provider) :
            base(context, provider){ }
    }
}