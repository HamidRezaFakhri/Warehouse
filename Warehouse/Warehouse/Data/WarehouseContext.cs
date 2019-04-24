namespace Warehouse.Models
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;

    public class WarehouseContext : DbContext
    {
        public WarehouseContext(DbContextOptions<WarehouseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.RemovePluralizingTableNameConvention();

            var implementedConfigTypes =
                Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace != null && !t.IsAbstract && !t.IsGenericTypeDefinition
                                          && t.GetTypeInfo().ImplementedInterfaces.Any(i =>
                                            i.GetTypeInfo().IsGenericType
                                            && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configType in implementedConfigTypes)
            {
                dynamic config = Activator.CreateInstance(configType);
                modelBuilder.ApplyConfiguration(config);
            }
        }

        public virtual DbSet<StoreDto> Store { get; set; }

        public virtual DbSet<Stuff> Stuffs { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<UserDto> Users { get; set; }

        public virtual DbSet<Remittance> Remittances { get; set; }

        public virtual DbSet<RemittanceStuff> RemittanceStuffs { get; set; }
    }
}