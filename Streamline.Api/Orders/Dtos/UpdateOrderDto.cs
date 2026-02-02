using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Streamline.API.Orders.Dtos
{
    public class UpdateOrderDto
    {
        [Required(ErrorMessage = "Products are required.")]
        [MinLength(1, ErrorMessage = "At least one product must be added to the order.")]
        public List<CreateOrderProductDto> Products { get; set; } = new();
    }
}
