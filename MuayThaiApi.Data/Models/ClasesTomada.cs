using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class ClasesTomada
    {
        public long ClaseId { get; set; }
        public long PagoId { get; set; }
        public DateTime FechaClase { get; set; }
        public bool Validada { get; set; }

        public virtual Pago Pago { get; set; }
    }
}
