using System.ComponentModel.DataAnnotations;

public class ToyCategory
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}
