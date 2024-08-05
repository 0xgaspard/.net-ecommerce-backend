using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
  public class CategoryClosure
  {
    public int AncestorId { get; set; }
    public Category Ancestor { get; set; }

    public int DescendantId { get; set; }
    public Category Descendant { get; set; }
  }
}