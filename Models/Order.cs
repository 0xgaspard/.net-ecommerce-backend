using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        public List<OrderDetail> ProductList { get; set; }
        
        [Required]
        public decimal TotalAmount { get; set; }
        
        [Required]
        public string PaymentMethod { get; set; }
        
        public string Status { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }

    public class OrderDetail
    {
         [Required]
        public int OrderId { get; set; } 

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        public Order Order { get; set; }  
        public Product Product { get; set; } 
    }
}