using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AuthenticateRequestBindingModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}