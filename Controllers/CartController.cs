using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Data;
using EcommerceBackend.DTOs;
using EcommerceBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : BaseController
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(CartDto cartDto)
        {
            var cart = new Cart
            {
                ProductId = cartDto.ProductId,
                Quantity = cartDto.Quantity,
                UserId = GetClientIdFromToken() 
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product added to cart" });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetCart()
        {
            var carts = await _context.Carts.Where(c => c.UserId == GetClientIdFromToken()).Include(c => c.Product).ToListAsync(); // Replace with actual user ID
            var cartDtos = carts.ConvertAll(c => new CartDto
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity
            });

            return Ok(cartDtos);
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> RemoveFromCart(int productId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == GetClientIdFromToken()); // Replace with actual user ID
            if (cart == null) return NotFound();

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product removed from cart" });
        }
    }
}