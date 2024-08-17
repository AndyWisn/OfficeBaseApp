using Microsoft.EntityFrameworkCore;
//using OfficeBaseApp.Components.CsvReader.Models;
using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Data;
public class OfficeBaseAppDbContext : DbContext
{
    public DbSet<ProductionPart> ProductionParts { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Product> Products { get; set; }

    //public DbSet<Car> Cars {  get; set; }

    public OfficeBaseAppDbContext(DbContextOptions<OfficeBaseAppDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
  

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);   
    //    modelBuilder.Entity<Customer>().ToTable("Customers");
    //    modelBuilder.Entity<Vendor>().ToTable("Vendors");
    //    modelBuilder.Entity<ProductionPart>().ToTable("ProductionParts");
    //    modelBuilder.Entity<Product>().ToTable("Products");
    //    modelBuilder.Entity<Customer>(entity =>
    //    {
    //        entity.HasKey(e => e.Id);
    //        entity.Property(e => e.Name)
    //         .HasMaxLength(100)
    //         .IsRequired();
    //        entity.Property(e => e.RepresentativeFirstName)
    //              .HasMaxLength(50);
    //        entity.Property(e => e.RepresentativeLastName)
    //              .HasMaxLength(50);
    //        entity.Property(e => e.Contact)
    //              .HasMaxLength(100);
    //    });
    //    modelBuilder.Entity<Vendor>(entity =>
    //    {
    //        entity.HasKey(e => e.Id);
    //        entity.Property(e => e.Name)
    //         .HasMaxLength(100);
    //        entity.Property(e => e.RepresentativeFirstName)
    //              .HasMaxLength(50);
    //        entity.Property(e => e.RepresentativeLastName)
    //              .HasMaxLength(50);
    //        entity.Property(e => e.Contact)
    //              .HasMaxLength(100);
    //        entity.Property(e => e.VendorCertificate)
    //         .HasMaxLength(50);
    //        entity.Property(e => e.SupportContact)
    //              .HasMaxLength(50);
    //    });
    //    modelBuilder.Entity<ProductionPart>(entity =>
    //    {
    //        entity.HasKey(e => e.Id);
    //        entity.Property(e => e.Name)
    //              .HasMaxLength(100);
    //        entity.Property(e => e.Description)
    //             .HasMaxLength(255);
    //        entity.Property(e => e.Price);
    //        entity.Property(e => e.ComponentVendorId);
    //    });
    //    modelBuilder.Entity<Product>(entity =>
    //    {
    //        entity.HasKey(e => e.Id);
    //        entity.Property(e => e.Name)
    //              .HasMaxLength(100)
    //              .IsRequired();
    //        entity.Property(e => e.Description)
    //              .HasMaxLength(255);
    //        entity.Property(e => e.ProductCost);
    //        entity.Property(e => e.ProductionComponentsId);
    //   });
    //}
}