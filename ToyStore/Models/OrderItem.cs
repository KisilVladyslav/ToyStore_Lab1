using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [Required]
    [ForeignKey("Toy")]
    public int ToyId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public virtual Order Order { get; set; }
    public virtual Toy Toy { get; set; }
}
