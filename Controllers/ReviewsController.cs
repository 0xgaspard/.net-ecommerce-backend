using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Data;
using EcommerceBackend.DTOs;
using EcommerceBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ReviewsController : BaseController
    {
        private readonly DataContext _context;

        public ReviewsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddReview(ReviewDto reviewDto)
        {
            var review = new Review
            {
                ProductId = reviewDto.ProductId,
                ClientId = GetClientIdFromToken(), // This should be replaced with the actual logged-in user ID
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review added successfully" });
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByProductId(int productId)
        {
            var reviews = await _context.Reviews.Where(r => r.ProductId == productId).ToListAsync();
            return Ok(reviews);
        }
    }
}