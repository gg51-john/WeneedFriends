using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class Memberjail
    {
        public int Jailid { get; set; }
        public int? Userid { get; set; }
        public string Reason { get; set; }
        public string Date { get; set; }
    }
}
