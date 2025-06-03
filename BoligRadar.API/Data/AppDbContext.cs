using BoligRadar.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoligRadar.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<SavedSearch> SavedSearches { get; set; }
    public DbSet<PriceHistory> PriceHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<User>()
            .HasIndex(u => u.GoogleId)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Area
        modelBuilder.Entity<Area>()
            .HasIndex(a => a.PostalCode)
            .IsUnique();

        // Property
        modelBuilder.Entity<Property>()
            .HasIndex(p => p.AdvertisementId)
            .IsUnique();

        modelBuilder.Entity<Property>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        // Price History
        modelBuilder.Entity<PriceHistory>()
            .Property(p => p.AveragePrice)
            .HasPrecision(18, 2);
        
        // Relationships
        modelBuilder.Entity<SavedSearch>()
            .HasOne(s => s.User)
            .WithMany(u => u.SavedSearches)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Property>()
            .HasOne(p => p.Area)
            .WithMany(a => a.Properties)
            .HasForeignKey(p => p.PostalCode);

        modelBuilder.Entity<PriceHistory>()
            .HasOne(p => p.Area)
            .WithMany(a => a.PriceHistories)
            .HasForeignKey(p => p.PostalCode);

    }
}