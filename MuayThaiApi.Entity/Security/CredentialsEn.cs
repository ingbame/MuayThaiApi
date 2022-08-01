using MuayThaiApi.Entity.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Entity.Security
{
    public class CredentialsEn
    {
        public CredentialsEn()
        {
            Persona = new PersonaEn();
            User = new UserEn();
        }
        public PersonaEn Persona { get; set; }
        public UserEn User { get; set; }
    }
}
