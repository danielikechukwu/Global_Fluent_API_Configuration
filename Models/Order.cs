using EF_FluentAPIGlobalConfiguration.Enums;

namespace EF_FluentAPIGlobalConfiguration.Models;

public class Order
{
    public int id { get; set; }
    
    public OrderStatus order_status { get; set; }
    
    public PaymentStatus payment_status { get; set; }
    
    public decimal total_amount { get; set; }
    
    public bool is_deleted { get; set; }
    
    public int customer_id { get; set; }
    
    public Customer customer { get; set; }

    public ICollection<OrderItem> order_items { get; set; } = new List<OrderItem>();
}