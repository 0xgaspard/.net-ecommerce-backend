namespace EcommerceBackend.DTOs
{
    public class ForgetPasswordDto
    {
        public string Email { get; set; }
        public string Recoverykey { get; set; }
        public string Newpassword { get; set; }
    }
}