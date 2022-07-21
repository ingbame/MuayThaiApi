using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class User
    {
        public User()
        {
            Afiliados = new HashSet<Afiliado>();
            PasswordsHistories = new HashSet<PasswordsHistory>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Afiliado> Afiliados { get; set; }
        public virtual ICollection<PasswordsHistory> PasswordsHistories { get; set; }
    }
}
