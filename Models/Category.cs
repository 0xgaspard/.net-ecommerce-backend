using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CategoryClosure> Ancestors { get; set; }
        public ICollection<CategoryClosure> Descendants { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}