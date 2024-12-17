using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
