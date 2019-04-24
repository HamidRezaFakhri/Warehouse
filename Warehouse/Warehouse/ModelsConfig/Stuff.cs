namespace Warehouse.ModelsConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Warehouse.Models;

    public class StuffConfig : IEntityTypeConfiguration<StuffDto>
    {
        public void Configure(EntityTypeBuilder<StuffDto> builder)
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
                .Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder
                .HasIndex(e => e.Code)
                .IsUnique();

            builder
                .HasMany(stuff => stuff.RemittanceStuffs)
                .WithOne(remittanceStuff => remittanceStuff.Stuff)
                .HasForeignKey(remittanceStuff => remittanceStuff.StuffId);

            builder
                .Property(e => e.InstanceId)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValue(Guid.NewGuid())
                .HasDefaultValueSql("NEWID()")
                .IsConcurrencyToken();

            builder.ToTable("Stuff", "dbo");
        }
    }
}