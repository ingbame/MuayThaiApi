using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Entity.Security
{
    public class JwtConfEn
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
