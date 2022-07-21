using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class Pago
    {
        public Pago()
        {
            ClasesTomada = new HashSet<ClasesTomada>();
        }

        public long PagoId { get; set; }
        public int AfiliadoId { get; set; }
        public DateTime FechaPago { get; set; }
        public bool Mensualidad { get; set; }
        public int? DiasPagados { get; set; }
        public long MetodoId { get; set; }
        public string EvidenciaUrl { get; set; }

        public virtual Afiliado Afiliado { get; set; }
        public virtual MetodosPago Metodo { get; set; }
        public virtual ICollection<ClasesTomada> ClasesTomada { get; set; }
    }
}
