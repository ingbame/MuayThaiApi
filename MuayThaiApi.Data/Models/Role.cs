﻿using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RolId { get; set; }
        public string RolDescription { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
