namespace Warehouse.ModelsConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Warehouse.Models;

    public class RemittanceStuffConfig : IEntityTypeConfiguration<RemittanceStuff>
    {
        public void Configure(EntityTypeBuilder<RemittanceStuff> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(e => new { e.RemittanceId, e.StuffId })
                .IsUnique();

            builder
                .Property(e => e.InstanceId)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValue(Guid.NewGuid())
                .HasDefaultValueSql("NEWID()")
                .IsConcurrencyToken();

            builder.ToTable("RemittanceStuff", "dbo");
        }
    }
}