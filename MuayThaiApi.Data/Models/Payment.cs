using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class Payment
    {
        public Payment()
        {
            ClassAttendances = new HashSet<ClassAttendance>();
        }

        public long PaymentId { get; set; }
        public long PersonId { get; set; }
        public DateTime FechaPago { get; set; }
        public bool Mensualidad { get; set; }
        public int? DiasPagados { get; set; }
        public int MethodId { get; set; }
        public string EvidenciaUrl { get; set; }

        public virtual PaymentMethod Method { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<ClassAttendance> ClassAttendances { get; set; }
    }
}
