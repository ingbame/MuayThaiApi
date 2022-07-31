using MuayThaiApi.Entity.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Entity.Security
{
    public class CredentialsDtoEn
    {
        public PersonaEn Persona { get; set; }
        public LoginDtoEn User { get; set; }
    }
}
