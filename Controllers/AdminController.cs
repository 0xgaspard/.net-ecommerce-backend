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
    // [Authorize]
    // [AdminAuthorize]
    public class AdminController : BaseController
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("categories")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto CategoryDto, [FromBody] int? parentId)
        {
            var category = new Category
            {
                Name = CategoryDto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Add self-referencing closure entry
            _context.CategoryClosures.Add(new CategoryClosure
            {
                AncestorId = category.Id,
                DescendantId = category.Id
            });

            // If a ParentId is specified, add closure entries for the new category
            if (parentId.HasValue)
            {

                var parentClosures = await _context.CategoryClosures
                    .Where(cc => cc.DescendantId == parentId)
                    .ToListAsync();

                foreach (var parentClosure in parentClosures)
                {
                    _context.CategoryClosures.Add(new CategoryClosure
                    {
                        AncestorId = parentClosure.AncestorId,
                        DescendantId = category.Id
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Category created successfully" });
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetFirstLayerCategories()
        {
            var firstLayerCategories = await _context.Categories
                .Where(c => !_context.CategoryClosures
                    .Any(cc => cc.DescendantId == c.Id && cc.AncestorId != c.Id))
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return Ok(firstLayerCategories);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetCategoryAll()
        {
            var categories = await _context.Categories
                .Include(c => c.Descendants)
                .ToListAsync();

            var categoryDict = categories.ToDictionary(c => c.Id, c => new NestedCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });

            foreach (var category in categories)
            {
                var subcategories = category.Descendants
                    .Where(cc => cc.AncestorId == category.Id && cc.AncestorId != cc.DescendantId)
                    .Select(cc => categoryDict[cc.DescendantId])
                    .ToList();

                categoryDict[category.Id].Subcategories.AddRange(subcategories);
            }

            var rootCategories = categoryDict.Values
                .Where(c => !categories.Any(cat => cat.Descendants.Any(cc => cc.DescendantId == c.Id && cc.AncestorId != c.Id)))
                .ToList();

            return Ok(rootCategories);
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetSubcategories(int id)
        {
            var subcategories = await _context.CategoryClosures
                .Where(cc => cc.AncestorId == id && cc.AncestorId != cc.DescendantId)
                .Include(cc => cc.Descendant)
                .Select(cc => new CategoryDto
                {
                    Id = cc.Descendant.Id,
                    Name = cc.Descendant.Name
                })
                .ToListAsync();

            return Ok(subcategories);
        }

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto CategoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = CategoryDto.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            if (_context.Products.Any(p => p.CategoryId == id))
            {
                return BadRequest("Cannot delete category with associated products.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var category = await _context.Categories
            .Include(c => c.Descendants)
            .FirstOrDefaultAsync(c => c.Id == productDto.CategoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            if (category.Descendants.Any(cc => cc.AncestorId != cc.DescendantId))
            {
                return BadRequest("");
            }

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                ImgUrl = productDto.ImgUrl,
                Quantity = productDto.Quantity,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
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

        [HttpGet("products/{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImgUrl = product.ImgUrl,
                Quantity = product.Quantity,
                Price = product.Price,
                CategoryId = product.CategoryId
            };

            return Ok(productDto);
        }

        [HttpGet("products/category/{id}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImgUrl = p.ImgUrl,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var category = await _context.Categories
            .Include(c => c.Descendants)
            .FirstOrDefaultAsync(c => c.Id == productDto.CategoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            if (category.Descendants.Any(cc => cc.AncestorId != cc.DescendantId))
            {
                return BadRequest("");
            }

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