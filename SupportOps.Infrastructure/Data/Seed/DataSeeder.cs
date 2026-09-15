using Microsoft.EntityFrameworkCore;
using SupportOps.Domain.Entities;
using SupportOps.Domain.Enums;
using AppEntity = SupportOps.Domain.Entities.Application;

namespace SupportOps.Infrastructure.Data.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!await context.Users.AnyAsync())
        {
            var admin = new User { Name = "Admin User", Email = "admin@supportops.local", Role = UserRole.ADMIN, CreatedAt = DateTime.UtcNow, IsActive = true };
            var analyst1 = new User { Name = "João Analista", Email = "joao@supportops.local", Role = UserRole.ANALISTA, CreatedAt = DateTime.UtcNow, IsActive = true };
            var requester1 = new User { Name = "Maria Cliente", Email = "maria@supportops.local", Role = UserRole.SOLICITANTE, CreatedAt = DateTime.UtcNow, IsActive = true };
            
            context.Users.AddRange(admin, analyst1, requester1);
            await context.SaveChangesAsync();
        }

        if (!await context.Applications.AnyAsync())
        {
            var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Role == UserRole.ADMIN);
            if (adminUser != null)
            {
                var app1 = new AppEntity { Name = "Portal de Vendas", Description = "Portal principal de vendas online", Owner = adminUser.Name, Environment = ApplicationEnvironment.PRODUCAO, CreatedAt = DateTime.UtcNow, Status = ApplicationStatus.ATIVO };
                var app2 = new AppEntity { Name = "ERP Interno", Description = "Sistema de gestão integrado", Owner = adminUser.Name, Environment = ApplicationEnvironment.PRODUCAO, CreatedAt = DateTime.UtcNow, Status = ApplicationStatus.ATIVO };

                context.Applications.AddRange(app1, app2);
                await context.SaveChangesAsync();
            }
        }
    }
}
