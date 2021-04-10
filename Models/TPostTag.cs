using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPostTag
    {
        public int FPosTagId { get; set; }
        public int? FPostId { get; set; }
        public int? FTagId { get; set; }

        public virtual TPost FPost { get; set; }
        public virtual TTag FTag { get; set; }
    }
}
