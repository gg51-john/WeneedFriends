using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TJoinWait
    {
        public int TWaitId { get; set; }
        public int? TPostId { get; set; }
        public int? TUserId { get; set; }

        public virtual TPost TPost { get; set; }
    }
}
