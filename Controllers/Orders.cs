using EF_FluentAPIGlobalConfiguration.Data;
using EF_FluentAPIGlobalConfiguration.DTOs;
using EF_FluentAPIGlobalConfiguration.Enums;
using EF_FluentAPIGlobalConfiguration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_FluentAPIGlobalConfiguration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Orders : ControllerBase
    {
        private readonly FluentAPIGlobalConfigurationDbContext _context;

        public Orders(FluentAPIGlobalConfigurationDbContext context)
        {
            _context = context;
        }

        // GET: api/<Orders>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Orders>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Orders> place an order
        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.customers.FirstOrDefaultAsync(c => c.id == request.customer_id);

            if (customer == null)
                return BadRequest("Customer does not exist or is inactive");

            if (request.items == null || !request.items.Any())
            {
                return BadRequest("Order must contain at least one item");
            }

            decimal total_amount = 0;

            var order_items = new List<OrderItem>();

            foreach (var item in request.items)
            {
                var product =
                    await _context.products.FirstOrDefaultAsync(p => p.id == item.product_id && !p.is_deleted);

                if (product == null)
                    return BadRequest($"ProductId {item.product_id} does not exist.)");

                if (item.quantity <= 0)
                    return BadRequest($"Invalid quantity for ProductId {item.product_id}. Quantity must be at least 1.");

                if (product.quantity < item.quantity)
                    return BadRequest($"Insufficient stock for product: {product.name}. Available: {product.quantity}");

                product.quantity -= item.quantity;

                var line_total = product.price * item.quantity;

                total_amount += line_total;

                order_items.Add(new OrderItem()
                {
                    product_id = product.id,
                    quantity = item.quantity,
                    line_total = line_total,
                });
            }

            // Create order entity
            var order = new Order()
            {
                customer_id = request.customer_id,
                total_amount = total_amount,
                order_status = OrderStatus.Pending,
                payment_status = PaymentStatus.Pending,
                is_deleted = false,
                order_items = order_items
            };

            _context.orders.Add(order);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Order placed successfully",
                order_id = order.id,
                total_amount = order.total_amount,
                items = order_items.Count
            });
        }

        // PUT api/<Orders>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Orders>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}