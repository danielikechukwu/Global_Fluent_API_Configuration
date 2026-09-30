using System.ComponentModel.DataAnnotations;

namespace EF_FluentAPIGlobalConfiguration.DTOs;

public class OrderRequestDTO
{
    [Required(ErrorMessage = "CustomerId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be greater than zero.")]
    public int customer_id { get; set; }
    
    [Required(ErrorMessage = "Items collection cannot be empty.")]
    [MinLength(1, ErrorMessage = "At least one order item is required.")]
    public List<OrderItemRequestDTO> items { get; set; }
}