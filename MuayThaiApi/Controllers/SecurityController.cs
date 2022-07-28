using Microsoft.AspNetCore.Mvc;
using MuayThaiApi.Core;
using MuayThaiApi.Entity.Security;

namespace MuayThaiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var prueba = CoSecurity.Instance.Prueba();
            return Ok(prueba);
        }
        [HttpPost]
        public ActionResult AltaAfiliado(_Afiliado persona)
        {
            if (persona == null)
                return NoContent();
            if (string.IsNullOrEmpty(persona.NombreAfiliado.Trim()))
                return BadRequest("Nombre de afiliado vacío.");
            if (persona.FechaNacimiento == null)
                return BadRequest("Revise su fecha de nacimiento.");
            if (persona.FechaNacimiento < DateTime.Parse("1940-01-01"))
                return BadRequest("Excede los 80 años, revise su fecha de nacimiento.");
            var personaResult = CoSecurity.Instance.AltaAfiliado(persona);
            return CreatedAtAction("GetAfiliado", new { id = personaResult.AfiliadoId });
        }
        [HttpGet("{id}")]
        public ActionResult<_AfiliadoDto> GetAfiliado(int? id)
        {
            if (id == null)
                return BadRequest("No se pudo buscar afiliado por id");            
            var afiliado = CoSecurity.Instance.GetAfiliado(id);
            if (afiliado == null)
                return NotFound();
            return afiliado;
        }
    }
}
