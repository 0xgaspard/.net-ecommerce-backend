using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : BaseController
    {
        [HttpGet]
        [Route("/")]  // This maps to the root URL
        public IActionResult Get()
        {
            return Content("Hello, World!", "text/plain");
        }
    }
}