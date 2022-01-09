using System.ComponentModel.DataAnnotations;

namespace Gift4U.BlazorApp.Data
{
    public class UserModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Name is too long.")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
