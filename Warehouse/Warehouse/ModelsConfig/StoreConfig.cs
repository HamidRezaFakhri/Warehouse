namespace Warehouse.ModelsConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Warehouse.Models;

    public class StoreConfig : IEntityTypeConfiguration<StoreDto>
    {
        public void Configure(EntityTypeBuilder<StoreDto> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder
                .HasIndex(e => e.Name)
                .IsUnique();

            builder
                .HasMany(store => store.Remittances)
                .WithOne(remittance => remittance.Store)
                .HasForeignKey(remittance => remittance.StoreId);

            builder
                .Property(e => e.InstanceId)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValue(Guid.NewGuid())
                .HasDefaultValueSql("NEWID()")
                .IsConcurrencyToken();

            builder.ToTable("Store", "dbo");
        }
    }
}