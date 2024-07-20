namespace EcommerceBackend.DTOs
{
    public class ForgetPasswordDto
    {
        public string Email { get; set; }
        public string RecoveryKey { get; set; }
        public string NewPassword { get; set; }
    }
}