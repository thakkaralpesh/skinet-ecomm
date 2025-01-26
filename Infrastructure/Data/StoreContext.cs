using Core.Entities;
using Infrastructure.config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
    }
}
