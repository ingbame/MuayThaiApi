using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class User
    {
        public User()
        {
            PasswordsHistories = new HashSet<PasswordsHistory>();
            Personas = new HashSet<Persona>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int RolId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Role Rol { get; set; }
        public virtual ICollection<PasswordsHistory> PasswordsHistories { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
    }
}
