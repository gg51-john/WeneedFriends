using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TLocation
    {
        public int FLocationId { get; set; }
        public int FCityId { get; set; }
        public string FDistrictName { get; set; }

        public virtual TCity FCity { get; set; }
    }
}
