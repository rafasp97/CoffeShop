using System.ComponentModel.DataAnnotations;

namespace Streamline.API.Customers.Dtos
{
    public class CreateCustomerDto
    {

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(30, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Document is required.")]
        [MaxLength(11, ErrorMessage = "Document cannot be longer than 11 characters.")]
        public string Document { get; set; } = null!;

        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(11, MinimumLength = 1, ErrorMessage = "Phone must be at most 11 characters.")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(50, ErrorMessage = "Email cannot be longer than 50 characters.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Neighborhood is required.")]
        public string Neighborhood { get; set; } = null!;

        [Required(ErrorMessage = "Number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number must be greater than zero.")]
        public int Number { get; set; }

        public string? Complement { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "State is required.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be exactly 2 characters.")]
        public string State { get; set; } = null!;
    }
}
