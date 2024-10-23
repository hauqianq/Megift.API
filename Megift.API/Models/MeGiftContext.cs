using Microsoft.EntityFrameworkCore;

namespace Megift.API.Models;

public partial class MeGiftContext : DbContext
{
    public MeGiftContext()
    {
            
    }
    public MeGiftContext(DbContextOptions<MeGiftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Image).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Sku).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Stock).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ShippingAddress).IsRequired().HasMaxLength(200);
            entity.HasMany(e => e.Orders).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerId);
            entity.HasMany(e => e.Reviews).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.OrderDate).IsRequired();
            entity.Property(e => e.Total).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Customer).WithMany(e => e.Orders).HasForeignKey(e => e.CustomerId);
            entity.HasOne(e => e.Product).WithMany(e => e.Orders).HasForeignKey(e => e.ProductId);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.HasOne(e => e.Customer).WithMany(e => e.Reviews).HasForeignKey(e => e.CustomerId);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=MeGift;User ID=sa;Password=12345;TrustServerCertificate=True");
    }
}