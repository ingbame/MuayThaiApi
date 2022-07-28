using Microsoft.AspNetCore.Mvc;
using MuayThaiApi.Core;

namespace MuayThaiApi.Controllers
{
    public class AppController : Controller
    {
        [HttpGet("{id}")]
        public ActionResult GetMenu(int id)
        {
            var menu = CoApplication.Instance.GetMenu(id);
            return Ok(menu);
        }
    }
}
