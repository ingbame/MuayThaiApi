using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class User
    {
        public User()
        {
            PasswordsHistories = new HashSet<PasswordsHistory>();
            People = new HashSet<Person>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool CanEdit { get; set; }
        public bool SavePasswords { get; set; }
        public bool? IsActive { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<PasswordsHistory> PasswordsHistories { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
