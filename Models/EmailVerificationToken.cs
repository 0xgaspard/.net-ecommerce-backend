namespace EcommerceBackend.Models
{
    public class EmailVerificationToken
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsVerified { get; set; }
    }
}
