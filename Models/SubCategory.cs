using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}