using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Cart
    {
        public int Id { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        
        public Product Product { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public int UserId { get; set; }
    }
}