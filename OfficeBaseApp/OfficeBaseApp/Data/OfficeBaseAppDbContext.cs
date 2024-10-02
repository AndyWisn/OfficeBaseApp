using Microsoft.EntityFrameworkCore;
using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Data;
public class OfficeBaseAppDbContext : DbContext
{
    public DbSet<ProductionPart> ProductionParts { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Product> Products { get; set; }
    public OfficeBaseAppDbContext(DbContextOptions<OfficeBaseAppDbContext> options)
        : base(options)
    {}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {}
}