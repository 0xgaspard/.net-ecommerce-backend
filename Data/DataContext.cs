using EcommerceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryClosure> CategoryClosures { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .HasMany(c => c.Ancestors)
            .WithOne(e => e.Descendant)
            .HasForeignKey(e => e.DescendantId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Descendants)
                .WithOne(e => e.Ancestor)
                .HasForeignKey(e => e.AncestorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoryClosure>()
                .HasKey(cc => new { cc.AncestorId, cc.DescendantId });

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@ecommerce.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = Role.Admin
            }

        );
            modelBuilder.Entity<EmailTemplate>().HasData(new EmailTemplate
            {
                Id = 1,
                Subject = "Please verify your email",
                Body = "<h1>Hello,</h1><p>Please verify your email by clicking the link below:</p><a href='{VerificationLink}'>Verify Email</a>"
            });
        }
    }
}