using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ProductsInventory.Api.Entities;

namespace ProductsInventory.Api.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50)]
        [Required]
        public required string Name { get; set; }

        public int? Quantity { get; set; }

        [Precision(6,2)]
        public double? Price { get; set; }

        // [JsonConverter(typeof(JsonStringEnumConverter))]
        // public List<Category>? Categories { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;

        public Product()
        {
            // Id = "0000";
            // Name = "UNKNOWN";
        }

        public Product(Guid id, string name, int quantity, double price)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
    
}