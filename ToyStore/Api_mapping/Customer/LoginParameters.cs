using System.ComponentModel.DataAnnotations;

namespace ToyStore.Api_mapping.Customer
{
    public class LoginParameters
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
