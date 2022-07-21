using Microsoft.AspNetCore.Mvc;
using MuayThaiApi.Core;

namespace MuayThaiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var prueba = Security.Instance.Prueba();
            return Ok(prueba);
        }
    }
}
