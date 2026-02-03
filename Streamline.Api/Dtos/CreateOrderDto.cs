using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Streamline.API.Dtos
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be greater than zero.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Products are required.")]
        [MinLength(1, ErrorMessage = "At least one product must be added to the order.")]
        public List<CreateOrderProductDto> Products { get; set; } = new();
    }
}
