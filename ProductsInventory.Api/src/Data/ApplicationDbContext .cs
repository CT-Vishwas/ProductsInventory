using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsInventory.Api.Entities;

namespace ProductsInventory.Api.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> products { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
: base(options)
    {
    }
    
 protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Product>()
        //     .Property(c => c.Categories)
        //     .HasConversion(
        //         new ValueConverter<List<Category>?, string>(
        //             value => JsonSerializer.Serialize(value ?? new List<Category>(), (JsonSerializerOptions?)null),
        //             static value => JsonSerializer.Deserialize<List<Category>>(value, (JsonSerializerOptions?)null) ?? new List<Category>()
        //         )
        //     )
        //     .Metadata.SetValueComparer(
        //         new ValueComparer<List<Category>?>(
        //             (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
        //             c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        //             c => c == null ? null : c.ToList()
        //         )
        //     );
    }
}