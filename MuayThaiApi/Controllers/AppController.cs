using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuayThaiApi.Core;

namespace MuayThaiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : Controller
    {
        [HttpGet("GetMenu"),Authorize]
        public ActionResult GetMenu([FromQuery] int id)
        {
            var menu = CoApplication.Instance.GetMenu(id);
            if (menu.Error)
                return BadRequest(menu.Message);
            return Ok(menu.Model);
        }
        #region Private Methods
        #endregion
    }
}
