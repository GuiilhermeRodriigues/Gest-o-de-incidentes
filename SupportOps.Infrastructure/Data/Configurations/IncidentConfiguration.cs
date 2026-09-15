using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportOps.Domain.Entities;

namespace SupportOps.Infrastructure.Data.Configurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Number).IsRequired().HasMaxLength(20);
        builder.HasIndex(i => i.Number).IsUnique();
        
        builder.Property(i => i.Title).IsRequired().HasMaxLength(200);
        builder.Property(i => i.Description).IsRequired();
        
        builder.HasOne(i => i.Application)
               .WithMany()
               .HasForeignKey(i => i.ApplicationId)
               .OnDelete(DeleteBehavior.Restrict);
               
        builder.HasOne(i => i.Requester)
               .WithMany()
               .HasForeignKey(i => i.RequesterId)
               .OnDelete(DeleteBehavior.Restrict);
               
        builder.HasOne(i => i.Assignee)
               .WithMany()
               .HasForeignKey(i => i.AssigneeId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.Property(i => i.Category).HasConversion<string>();
        builder.Property(i => i.Impact).HasConversion<string>();
        builder.Property(i => i.Urgency).HasConversion<string>();
        builder.Property(i => i.Priority).HasConversion<string>();
        builder.Property(i => i.Status).HasConversion<string>();
    }
}
