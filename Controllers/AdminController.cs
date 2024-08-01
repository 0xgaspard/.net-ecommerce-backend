using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Data;
using EcommerceBackend.DTOs;
using EcommerceBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;

namespace EcommerceBackend.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    [AdminAuthorize]
    public class AdminController : BaseController
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            Console.WriteLine("I AM HERERER😂");
            Console.WriteLine(categoryDto);
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category created successfully" });
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> ListCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            var categoryDtos = categories.ConvertAll(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });

            return Ok(categoryDtos);
        }

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category updated successfully" });
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category deleted successfully" });
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                ImgUrl = productDto.ImgUrl,
                Quantity = productDto.Quantity,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product created successfully" });
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> ListProducts()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            var productDtos = products.ConvertAll(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ImgUrl = p.ImgUrl,
                Quantity = p.Quantity,
                Price = p.Price,
                CategoryId = p.CategoryId
            });

            return Ok(productDtos);
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.ImgUrl = productDto.ImgUrl;
            product.Quantity = productDto.Quantity;
            product.Price = productDto.Price;
            product.CategoryId = productDto.CategoryId;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product updated successfully" });
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully" });
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> ListOrders()
        {
            var orders = await _context.Orders.Include(o => o.ProductList).ThenInclude(od => od.Product).ToListAsync();
            var orderDtos = orders.ConvertAll(o => new OrderDto
            {
                Id = o.Id,
                ProductList = o.ProductList.Select(od => new OrderDetailDto
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity
                }).ToList(),
                TotalAmount = o.TotalAmount,
                Status = o.Status
            });

            return Ok(orderDtos);
        }

        [HttpPut("orders/{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatusUpdateDto statusUpdateDto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Status = statusUpdateDto.Status;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order status updated successfully" });
        }

        [HttpGet("analytics/sales")]
        public async Task<ActionResult<IEnumerable<ProductSalesDto>>> GetSales([FromQuery] string startDate, [FromQuery] string endDate)
        {
            if (!DateTime.TryParse(startDate, out DateTime parsedStartDate))
            {
                return BadRequest("Invalid startDate format. Please use a valid date format.");
            }

            if (!DateTime.TryParse(endDate, out DateTime parsedEndDate))
            {
                return BadRequest("Invalid endDate format. Please use a valid date format.");
            }

            // Ensure the DateTime objects are in UTC
            parsedStartDate = DateTime.SpecifyKind(parsedStartDate, DateTimeKind.Utc);
            parsedEndDate = DateTime.SpecifyKind(parsedEndDate, DateTimeKind.Utc);

            var sales = await _context.Products
                .Where(p => p.OrderDetails.Any(od => od.Order.CreatedAt >= parsedStartDate && od.Order.CreatedAt <= parsedEndDate))
                .Select(p => new ProductSalesDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImgUrl = p.ImgUrl,
                    SalesAmount = p.OrderDetails.Sum(od => od.Quantity * od.Price),
                    Category = p.Category.Name
                })
                .ToListAsync();

            return Ok(sales);
        }
    }
}