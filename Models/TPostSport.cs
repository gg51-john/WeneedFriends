using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPostSport
    {
        public int FPostSportId { get; set; }
        public int? FPostId { get; set; }
        public string FSportName { get; set; }

        public virtual TPost FPost { get; set; }
    }
}
