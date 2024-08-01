using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Data;
using EcommerceBackend.DTOs;
using EcommerceBackend.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly DataContext _context;

        public ProfileController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetProfile()
        {
            var userId = GetClientIdFromToken();
            var user = await _context.Users
                                     .Where(u => u.Id == userId)
                                     .Select(u => new ProfileResponseDto
                                     {
                                         Id = u.Id,
                                         Username = u.Username,
                                         Email = u.Email,
                                         Role = u.Role
                                     })
                                     .FirstOrDefaultAsync();
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProfile(ProfileDto profileDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetClientIdFromToken()); // Replace with actual user ID
            if (user == null) return NotFound();

            user.Username = profileDto.Username;
            user.Email = profileDto.Email;
            user.Password = profileDto.Password;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Profile updated successfully" });
        }
    }
}