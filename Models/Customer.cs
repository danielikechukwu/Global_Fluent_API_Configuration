namespace EF_FluentAPIGlobalConfiguration.Models;

public class Customer
{
    public int id { get; set; }
    
    public string full_name { get; set; }
    
    public string email { get; set; }
    
    public bool is_deleted { get; set; }
    
    public ICollection<Order> orders { get; set; }
    
}