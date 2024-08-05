using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Data;
using EcommerceBackend.DTOs;
using EcommerceBackend.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder(OrderDto orderDto)
        {
            Console.WriteLine(GetClientIdFromToken());
            var order = new Order
            {
                UserId = GetClientIdFromToken(), // This should be replaced with the actual logged-in user ID
                ProductList = orderDto.ProductList.Select(p => new OrderDetail
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    Price = _context.Products.FirstOrDefault(prod => prod.Id == p.ProductId)?.Price ?? 0
                }).ToList(),
                TotalAmount = orderDto.TotalAmount,
                PaymentMethod = orderDto.PaymentMethod,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order placed successfully" });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            Console.WriteLine("Here in the get request");
            var order = await _context.Orders.Include(o => o.ProductList).ThenInclude(p => p.Product).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return NotFound();

            var orderDto = new OrderDto
            {
                Id = order.Id,
                ProductList = order.ProductList.Select(p => new OrderDetailDto
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList(),
                TotalAmount = order.TotalAmount,
                PaymentMethod = order.PaymentMethod,
                Status = order.Status
            };

            return Ok(orderDto);
        }
    }
}