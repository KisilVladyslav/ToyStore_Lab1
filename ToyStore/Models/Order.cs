using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; }

    // Колекція для зв'язку з деталями замовлення (OrderItem)
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Customer Customer { get; set; }
}