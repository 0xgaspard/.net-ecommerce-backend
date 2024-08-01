namespace EcommerceBackend.DTOs
{
    public class ReviewDto
    {
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}