using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuayThaiApi.Core;
using MuayThaiApi.Entity.Application;
using MuayThaiApi.Entity.Business;
using MuayThaiApi.Entity.Security;
using System.Security.Claims;

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
        private CredentialsDtoEn GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var credentialClaims = identity.Claims;

                return new CredentialsDtoEn
                {
                    Persona = new PersonaEn
                    {
                        NombreCompleto = credentialClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
                    },
                    User = new LoginDtoEn { 
                        UserName = credentialClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value
                    }
                };
            }
            return default(CredentialsDtoEn);
        }
        #endregion
    }
}
