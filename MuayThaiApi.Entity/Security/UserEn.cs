using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Entity.Security
{
    public class UserEn
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int? RolId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
