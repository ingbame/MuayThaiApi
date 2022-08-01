using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuayThaiApi.Core;
using MuayThaiApi.Entity.Security;

namespace MuayThaiApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private IConfiguration _configuration;
        public SecurityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost, AllowAnonymous]
        public ActionResult LoginAuthentication(LoginEn user)
        {
            if (string.IsNullOrEmpty(user.UserName))
                return BadRequest("Nombre de usuario vacío");
            if (string.IsNullOrEmpty(user.Password))
                return BadRequest("Contraseña vacía");

            var jwtConf = new JwtConfEn
            {
                Key = _configuration["Jwt:Key"],
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var response = SecurityBo.Instance.LoginAuthentication(user, jwtConf);

            if (response.Error)
                return BadRequest(response.Message);

            return Ok(response.Model);
        }
        [HttpPost]
        public ActionResult CreateNewPerson(CredentialsEn credential)
        {
            if (credential == null)
                return NoContent();

            if (string.IsNullOrEmpty(credential.User.UserName.Trim()))
                return BadRequest("Nombre de usuario vacío.");

            if (string.IsNullOrEmpty(credential.User.Password.Trim()))
                return BadRequest("Contraseña vacía.");

            if (string.IsNullOrEmpty(credential.Persona.FullName.Trim()))
                return BadRequest("Nombre de persona vacío.");

            if (string.IsNullOrEmpty(credential.Persona.NickName.Trim()))
                credential.Persona.NickName = credential.User.UserName;

            if (credential.Persona.Birthday == null)
                return BadRequest("Revise su fecha de nacimiento.");

            if (credential.Persona.Birthday < DateTime.Parse("1940-01-01"))
                return BadRequest("Excede los 80 años, revise su fecha de nacimiento.");

            if (string.IsNullOrEmpty(credential.Persona.CellPhoneNumber.Trim()))
                return BadRequest("Número de celular vacío.");
            
            var personaResult = SecurityBo.Instance.CreateNewPerson(credential);
            if (personaResult.Error)
                return BadRequest(personaResult.Message);
            return Ok(personaResult.Model);
        }
    }
}
