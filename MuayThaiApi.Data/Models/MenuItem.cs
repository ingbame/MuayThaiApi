using System;
using System.Collections.Generic;

namespace MuayThaiApi.Data.Models
{
    public partial class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }
        public string TargetPage { get; set; }
    }
}
