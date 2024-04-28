using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Oasis.API.DTOs;

public class CustomerSignInDto
{
    [Required]
    [EmailAddress]
    [DefaultValue("pk@gmail.com")]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [DefaultValue("2004")]
    public string Password { get; set; } = null!;
}
