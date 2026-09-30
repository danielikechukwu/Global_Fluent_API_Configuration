namespace EF_FluentAPIGlobalConfiguration.Models;

public class Product
{
    public int id { get; set; }
    
    public string name { get; set; }
    
    public decimal price { get; set; }
    
    public int quantity { get; set; }
    
    public bool is_deleted { get; set; }
}