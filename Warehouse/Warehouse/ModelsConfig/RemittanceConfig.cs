namespace Warehouse.ModelsConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Warehouse.Models;

    public class RemittanceConfig : IEntityTypeConfiguration<Remittance>
    {
        public void Configure(EntityTypeBuilder<Remittance> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder
                .HasIndex(e => e.Code)
                .IsUnique();

            builder
                .Property(e => e.InDate)
                .HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()");
            
            builder
                .Property(e => e.RemittanceType)
                .IsRequired();

            builder
                .Property(e => e.StoreId)
                .IsRequired();

            builder
                .Property(e => e.UserId)
                .IsRequired();

            builder
                .Property(e => e.Description)
                .IsUnicode();

            builder
                .HasMany(remittance => remittance.RemittanceStuffs)
                .WithOne(remittanceStuff => remittanceStuff.Remittance)
                .HasForeignKey(remittanceStuff => remittanceStuff.RemittanceId);

            builder
                .Property(e => e.InstanceId)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValue(Guid.NewGuid())
                .HasDefaultValueSql("NEWID()")
                .IsConcurrencyToken();

            builder.ToTable("Remittance", "dbo");
        }
    }
}