namespace EF_FluentAPIGlobalConfiguration.Models;

public class OrderItem
{
    public int id { get; set; }
    
    public int quantity { get; set; }
    
    public decimal line_total { get; set; }
    
    public bool is_deleted { get; set; }
    
    public int order_id { get; set; }
    
    public Order order { get; set; } = null!;
    
    public int product_id { get; set; }
    
    public Product product { get; set; } = null!;
}