namespace EcommerceBackend.DTOs
{
    public class ProductSalesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public decimal SalesAmount { get; set; }
        public string Category { get; set; }
    }
}