using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class AssignRoleMenu
    {
        public int RolId { get; set; }
        public int MenuItemId { get; set; }

        public virtual MenuItem MenuItem { get; set; }
        public virtual Role Rol { get; set; }
    }
}
