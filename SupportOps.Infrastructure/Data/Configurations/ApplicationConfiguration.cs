using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportOps.Domain.Entities;
using AppEntity = SupportOps.Domain.Entities.Application;

namespace SupportOps.Infrastructure.Data.Configurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<AppEntity>
{
    public void Configure(EntityTypeBuilder<AppEntity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Environment).HasConversion<string>();
        builder.Property(a => a.Status).HasConversion<string>();
    }
}
