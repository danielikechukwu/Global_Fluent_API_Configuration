using EF_FluentAPIGlobalConfiguration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EF_FluentAPIGlobalConfiguration.Data;

public class FluentAPIGlobalConfigurationDbContext : DbContext
{
    public DbSet<Customer> customers { get; set; } = null!;
    public DbSet<Product> products { get; set; } = null!;
    public DbSet<Order> orders { get; set; } = null!;
    public DbSet<OrderItem> order_items { get; set; }
    
    public FluentAPIGlobalConfigurationDbContext(DbContextOptions<FluentAPIGlobalConfigurationDbContext> options) : base(options){}

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Allowing database to store enums in string
        configurationBuilder.Properties<Enum>()
            .HaveConversion(typeof(EnumToStringConverter<>));
        
        // Sets default decimal precision and scale for all decimal properties
        // Example: decimal(18, 2)
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 2);

        // Configure global max length for all string properties
        configurationBuilder.Properties<string>()
            .HaveMaxLength(200);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema("sales");

        modelBuilder.Entity<Order>(entity =>
        {
            
            entity.HasKey(o => o.id);
            
            entity.HasOne(o => o.customer)
                .WithMany(c => c.orders)
                .HasForeignKey(o => o.customer_id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {

            entity.HasKey(oi => oi.id);
            
            entity.HasOne(oi => oi.order)
                .WithMany(o => o.order_items)
                .HasForeignKey(oi => oi.order_id)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(oi => oi.product)
                .WithMany()
                .HasForeignKey(oi => oi.product_id)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // Fetch all foreign keys across all entities in the model
        var foerign_keys = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys());

        foreach (var foerign_key in foerign_keys)
        {
            foerign_key.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
       // Add global shadow properties for CreatedAt and UpdatedAt
       foreach (var entity_property in modelBuilder.Model.GetEntityTypes())
       {
           if (entity_property.IsOwned() || entity_property.FindPrimaryKey() == null || entity_property.IsAbstract())
               continue;

           var clr_property = entity_property.ClrType.GetProperties();
           
           bool has_created_at = clr_property.Any(p => p.Name == "created_at");
           bool has_updated_at = clr_property.Any(p => p.Name == "updated_at");

           if (!has_created_at) 
               modelBuilder.Entity(entity_property.ClrType).Property<DateTime>("created_at").IsRequired();
           
           if(!has_updated_at)
               modelBuilder.Entity(entity_property.ClrType).Property<DateTime>("updated_at").IsRequired();
       }
       
       modelBuilder.Entity<Customer>().HasData(
           new { id = 1, full_name = "Pranaya Kumar Rout", email = "pranaya@example.com", is_deleted = false, created_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc), updated_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
           new { id = 2, full_name = "John Doe", email = "john.doe@example.com", is_deleted = false, created_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc), updated_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc) }
       );
       
       modelBuilder.Entity<Product>().HasData(
           new { id = 1, name = "Dell Laptop", price = 55000.00m, quantity = 10, is_deleted = false, created_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc), updated_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
           new { id = 2, name = "Wireless Mouse", price = 1200.00m, quantity = 50, is_deleted = false, created_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc), updated_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc) },
           new { id = 3, name = "Mechanical Keyboard", price = 3500.00m, quantity = 25, is_deleted = false, created_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc), updated_at = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc) }
       );
    }
    
    // Ensure synchronous calls also populate them
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var changed_entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
           
        var utc_now = DateTime.UtcNow;
           
        foreach (var entry in changed_entries)
        {
            if (entry.State == EntityState.Added && entry.Properties.Any(p => p.Metadata.Name == "created_at"))
            {
                entry.Property("created_at").CurrentValue = utc_now;
            }
               
            if (entry.State == EntityState.Added && entry.Properties.Any(p => p.Metadata.Name == "updated_at"))
            {
                entry.Property("updated_at").CurrentValue = utc_now;
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    // Ensure synchronous calls also populate them
    public override int SaveChanges()
    {
        var changed_entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
           
        var utc_now = DateTime.UtcNow;
           
        foreach (var entry in changed_entries)
        {
            if (entry.State == EntityState.Added && entry.Properties.Any(p => p.Metadata.Name == "created_at"))
            {
                entry.Property("created_at").CurrentValue = utc_now;
            }
               
            if (entry.State == EntityState.Added && entry.Properties.Any(p => p.Metadata.Name == "updated_at"))
            {
                entry.Property("updated_at").CurrentValue = utc_now;
            }
        }
           
        return base.SaveChanges();
    }
}