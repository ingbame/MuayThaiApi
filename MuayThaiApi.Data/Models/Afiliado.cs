using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class Afiliado
    {
        public Afiliado()
        {
            Pagos = new HashSet<Pago>();
        }

        public int AfiliadoId { get; set; }
        public int UserId { get; set; }
        public string NombreAfiliado { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
