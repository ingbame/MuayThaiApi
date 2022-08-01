using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuayThaiApi.ApiCommon;
using MuayThaiApi.Core;

namespace MuayThaiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : Controller
    {
        [HttpGet("GetMenu"),Authorize]
        public ActionResult GetMenu()
        {
            var currentUser = Session.Instance.GetCurrentUser(HttpContext);
            var menu = ApplicationBo.Instance.GetMenu(currentUser.User.RoleDescription);
            if (menu.Error)
                return BadRequest(menu.Message);
            return Ok(menu.Model);
        }
        #region Private Methods
        #endregion
    }
}
