using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Toy
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required]
    public bool IsAvailable { get; set; }  // Якщо товар доступний, то true, інакше false.

    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }

    public virtual ToyCategory Category { get; set; }
}
