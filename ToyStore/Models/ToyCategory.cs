using System.ComponentModel.DataAnnotations;

public class ToyCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}
