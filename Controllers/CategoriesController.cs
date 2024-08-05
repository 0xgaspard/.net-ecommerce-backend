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
    //[ApiController]
    //[Authorize]
    public class CategoriesController : BaseController
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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
    }
}