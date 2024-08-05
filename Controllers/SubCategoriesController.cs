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
    public class SubCategoryController : BaseController
    {
        private readonly DataContext _context;

        public SubCategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetSubCategories()
        {
            var subCategories = await _context.SubCategories.ToListAsync();
            var subCategoryDtos = subCategories.ConvertAll(sc => new SubCategoryDto
            {
                Id = sc.Id,
                Name = sc.Name,
                CategoryId = sc.CategoryId
            });

            return Ok(subCategoryDtos);
        }

        [HttpGet("{categoryId}")]
       public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var subCategories = await _context.SubCategories
                                              .Where(sc => sc.CategoryId == categoryId)
                                              .ToListAsync();
            var subCategoryDtos = subCategories.ConvertAll(sc => new SubCategoryDto
            {
                Id = sc.Id,
                Name = sc.Name,
                CategoryId = sc.CategoryId
            });

            return Ok(subCategoryDtos);
        }

    }
}