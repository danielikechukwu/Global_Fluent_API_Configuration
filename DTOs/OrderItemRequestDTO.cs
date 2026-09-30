using System.ComponentModel.DataAnnotations;

namespace EF_FluentAPIGlobalConfiguration.DTOs;

public class OrderItemRequestDTO
{
    [Required(ErrorMessage = "ProductId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than zero.")]
    public int product_id { get; set; }
    
    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int quantity { get; set; }
}