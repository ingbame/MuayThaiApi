using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Entity.Business
{
    public class PersonaEn
    {
        public long? PersonId { get; set; }
        public long? UserId { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string CellPhoneNumber { get; set; }
    }
}
