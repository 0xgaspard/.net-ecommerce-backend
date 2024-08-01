namespace EcommerceBackend.DTOs
{
    public class ProfileResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public EcommerceBackend.Models.Role Role { get; set; }
    }
}