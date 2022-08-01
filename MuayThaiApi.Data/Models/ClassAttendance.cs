using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class ClassAttendance
    {
        public long ClassId { get; set; }
        public long PaymentId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public bool Verified { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
