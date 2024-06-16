using Microsoft.EntityFrameworkCore;
using OfficeBaseApp.Entities;
namespace OfficeBaseApp.Data;

public class OfficeBaseAppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Component> Components => Set<Component>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Product> Products => Set<Product>();

    public OfficeBaseAppDbContext()
    {
        //this.Database.EnsureDeleted();
        this.Database.EnsureCreated();
        //this.SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OfficeBaseApp;Trusted_Connection=True;ConnectRetryCount=0");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Vendor>().ToTable("Vendors");
        modelBuilder.Entity<Component>().ToTable("Components");
        modelBuilder.Entity<Product>().ToTable("Products");

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
             .HasMaxLength(100)
             .IsRequired();
            entity.Property(e => e.RepresentativeFirstName)
                  .HasMaxLength(50);
            entity.Property(e => e.RepresentativeLastName)
                  .HasMaxLength(50);
            entity.Property(e => e.Contact)
                  .HasMaxLength(100);
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
             .HasMaxLength(100);
            entity.Property(e => e.RepresentativeFirstName)
                  .HasMaxLength(50);
            entity.Property(e => e.RepresentativeLastName)
                  .HasMaxLength(50);
            entity.Property(e => e.Contact)
                  .HasMaxLength(100);
            entity.Property(e => e.VendorCertificate)
             .HasMaxLength(50);
            entity.Property(e => e.SupportContact)
                  .HasMaxLength(50);
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                  .HasMaxLength(100);
            entity.Property(e => e.Description)
                 .HasMaxLength(255);
            entity.Property(e => e.Price);
            entity.Property(e => e.ComponentVendorId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                  .HasMaxLength(100)
                  .IsRequired();
            entity.Property(e => e.Description)
                  .HasMaxLength(255);
            entity.Property(e => e.ProductCost);
            entity.Property(e => e.ProductionComponentsId);
       });
    }
}