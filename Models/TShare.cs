using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TShare
    {
        public int ShareId { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public int? UseredId { get; set; }

        public virtual TPost Post { get; set; }
    }
}
