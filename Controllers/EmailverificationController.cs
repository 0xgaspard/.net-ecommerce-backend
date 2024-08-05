using EcommerceBackend.Models;
using EcommerceBackend.Services;
using EcommerceBackend.Data;
using Microsoft.AspNetCore.Mvc;
using EcommerceBackend.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceBackend.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
    // [Authorize]
    public class EmailVerificationController : BaseController
    {
        private readonly DataContext _context;
        private readonly EmailService _emailService;

        public EmailVerificationController(DataContext context)
        {
            _context = context;
            _emailService = new EmailService(context);
        }

        [HttpPost("send")]
        public IActionResult SendVerificationEmail(EmailVerificationDto emailVerificationDto)
        {
            Console.WriteLine("I see you here, ☺");
            var token = Guid.NewGuid().ToString();
            var expiration = DateTime.UtcNow.AddHours(24);

            var emailVerificationToken = new EmailVerificationToken
            {
                Email = emailVerificationDto.Email,
                Token = token,
                Expiration = expiration,
                IsVerified = false
            };

            Console.WriteLine("=========");
            Console.WriteLine(emailVerificationToken);

            _context.EmailVerificationTokens.Add(emailVerificationToken);
            _context.SaveChanges();

            _emailService.SendVerificationEmail(emailVerificationDto.Email, token);
            return Ok(new { Message = "Verification email sent" });
        }

        [HttpGet("verify")]
        public IActionResult VerifyEmail(string email, string token)
        {
            var emailVerificationToken = _context.EmailVerificationTokens
                .FirstOrDefault(e => e.Email == email && e.Token == token);

            if (emailVerificationToken == null || emailVerificationToken.Expiration < DateTime.UtcNow)
            {
                return BadRequest(new { Message = "Invalid or expired token" });
            }

            emailVerificationToken.IsVerified = true;
            _context.SaveChanges();

            return Ok(new { Message = "Email verified successfully. You can log in now." });
        }
    }
}
