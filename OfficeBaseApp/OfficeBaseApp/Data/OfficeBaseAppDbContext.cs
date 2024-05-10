namespace OfficeBaseApp.Data;
using OfficeBaseApp.Entities;
using Microsoft.EntityFrameworkCore;
public class OfficeBaseAppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Component> Components => Set<Component>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("OfficeBaseAppDb");
    }
}