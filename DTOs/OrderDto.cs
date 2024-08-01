using System.Collections.Generic;

namespace EcommerceBackend.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public List<OrderDetailDto> ProductList { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}