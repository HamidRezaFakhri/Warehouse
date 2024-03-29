﻿namespace Warehouse.ModelsConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Warehouse.Models;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder
                .Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder
                .Property(e => e.RoleId)
                .IsRequired();

            builder
                .HasIndex(e => e.UserName)
                .IsUnique();

            builder
                .Property(e => e.InstanceId)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValue(Guid.NewGuid())
                .HasDefaultValueSql("NEWID()")
                .IsConcurrencyToken();

            builder
                .HasMany(user => user.Remittances)
                .WithOne(remittance => remittance.User)
                .HasForeignKey(remittance => remittance.UserId);

            builder.ToTable("User", "sec");
        }
    }
}