using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Payments = new HashSet<Payment>();
        }

        public int MethodId { get; set; }
        public string MethodDesc { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
