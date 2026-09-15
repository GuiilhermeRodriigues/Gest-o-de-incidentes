using Microsoft.EntityFrameworkCore;
using SupportOps.Domain.Entities;
using AppEntity = SupportOps.Domain.Entities.Application;
using SupportOps.Domain.Entities;

namespace SupportOps.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<AppEntity> Applications => Set<AppEntity>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<IncidentHistory> IncidentHistories => Set<IncidentHistory>();
    public DbSet<KnowledgeArticle> KnowledgeArticles => Set<KnowledgeArticle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
