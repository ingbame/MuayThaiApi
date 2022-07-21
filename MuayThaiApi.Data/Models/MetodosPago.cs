using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class MetodosPago
    {
        public MetodosPago()
        {
            Pagos = new HashSet<Pago>();
        }

        public long MetodoId { get; set; }
        public string MetodoDesc { get; set; }

        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
