using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("Order")]
    public Guid OrderId { get; set; }

    [Required]
    [ForeignKey("Toy")]
    public Guid ToyId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    public virtual Order Order { get; set; }
    public virtual Toy Toy { get; set; }
}
