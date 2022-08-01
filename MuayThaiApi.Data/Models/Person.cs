using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class Person
    {
        public Person()
        {
            Payments = new HashSet<Payment>();
        }

        public long PersonId { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string CellPhoneNumber { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
