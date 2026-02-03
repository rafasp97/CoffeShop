using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Streamline.API.Dtos
{

    public class CreateOrderProductDto
    {
        [Required(ErrorMessage = "ProductId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than zero.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }
    }
}
