using System.ComponentModel.DataAnnotations;
using EcommerceBackend.Models;

namespace EcommerceBackend.Models
{
    public class Review
    {
        public int Id { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        public Product Product { get; set; }
        
        [Required]
        public int ClientId { get; set; }
        
        public User Client { get; set; }
        
        [Required]
        public int Rating { get; set; }
        
        public string Comment { get; set; }
    }
}