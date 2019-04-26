namespace Warehouse.ModelsConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Warehouse.Models;

    public class WSLogConfig : IEntityTypeConfiguration<WSLog>
    {
        public void Configure(EntityTypeBuilder<WSLog> builder)
        {
            builder
                .Property(e => e.Message)
                .IsUnicode();

            builder
                .Property(e => e.MessageTemplate)
                .IsUnicode();

            builder
                .Property(e => e.Level)
                .HasMaxLength(128)
                .IsUnicode();

            builder
                .Property(e => e.TimeStamp)
                .IsRequired();

            builder
                .Property(e => e.Exception)
                .IsUnicode();

            builder
                .Property(e => e.Properties)
                .HasColumnType("xml");

            builder
                .Property(e => e.LogEvent)
                .IsUnicode();

            builder
                .HasIndex(e => e.TimeStamp);

            builder
                .Property(e => e.OtherData)
                .HasMaxLength(50);

            builder.ToTable("Logs", "dbo");
        }
    }
}