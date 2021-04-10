using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class ChangeStatusDate
    {
        public int Changeid { get; set; }
        public int? Userid { get; set; }
        public string Recoverytime { get; set; }
        public bool? Status { get; set; }
    }
}
