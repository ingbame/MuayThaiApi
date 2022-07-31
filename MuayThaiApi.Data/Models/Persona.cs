using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Pagos = new HashSet<Pago>();
        }

        public int PersonaId { get; set; }
        public int UserId { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
