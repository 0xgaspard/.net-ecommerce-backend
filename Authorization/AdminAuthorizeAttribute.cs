using Microsoft.AspNetCore.Authorization;

public class AdminAuthorizeAttribute : AuthorizeAttribute
{
    public AdminAuthorizeAttribute()
    {
        Policy = "AdminPolicy";
    }
}