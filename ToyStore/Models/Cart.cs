using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Cart
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CustomerId { get; set; }  // Ідентифікатор клієнта, якому належить кошик

    public virtual Customer Customer { get; set; }  // Навігаційна властивість для клієнта

    public virtual ICollection<CartItem> CartItems { get; set; }  // Колекція товарів у кошику

    public decimal TotalPrice  // Загальна ціна всіх товарів у кошику
    {
        get
        {
            decimal total = 0;
            foreach (var item in CartItems)
            {
                total += item.Quantity * item.Toy.Price;
            }
            return total;
        }
    }
}
