using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace EcommerceBackend.Controllers
{
  [ApiController]
  public abstract class BaseController : ControllerBase
  {
    protected int GetClientIdFromToken()
    {
      var claimsIdentity = User.Identity as ClaimsIdentity;
      var clientIdClaim = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
      if (clientIdClaim == null)
      {
        throw new UnauthorizedAccessException("Client ID claim not found.");
      }
      return int.Parse(clientIdClaim.Value);
    }
  }
}