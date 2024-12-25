using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("Cart")]
    public Guid CartId { get; set; }  // Ідентифікатор кошика, до якого належить товар

    [Required]
    [ForeignKey("Toy")]
    public Guid ToyId { get; set; }  // Ідентифікатор товару

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }  // Кількість одиниць товару в кошику

    public virtual Cart Cart { get; set; }  // Навігаційна властивість для кошика
    public virtual Toy Toy { get; set; }  // Навігаційна властивість для товару
}
