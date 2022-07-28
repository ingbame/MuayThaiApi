using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Entity.Security
{
    public class _AfiliadoDto
    {
        public int? AfiliadoId { get; set; }
        public string NombreAfiliado { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public _UserDto Credenciales { get; set; }
    }
}
