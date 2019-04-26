namespace Warehouse.ModelsConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Warehouse.Models;

    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
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
                .HasMany(role => role.Users)
                .WithOne(user => user.Role)
                .HasForeignKey(user => user.RoleId);

            builder
                .Property(e => e.InstanceId)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValue(Guid.NewGuid())
                .HasDefaultValueSql("NEWID()")
                .IsConcurrencyToken();

            builder.ToTable("Role", "sec");
        }
    }
}