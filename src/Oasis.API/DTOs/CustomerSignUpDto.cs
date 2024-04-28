using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Oasis.API.DTOs {
    public class CustomerSignUpDto
    {
        [Required]
        [EmailAddress]
        [DefaultValue("pk@gmail.com")]
        public required string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DefaultValue("Patrick Reis")]

        public required string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DefaultValue("2004")]
        public required string Password { get; set; }
    }
}

