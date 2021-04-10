using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TJoinPerson
    {
        public int FJoinId { get; set; }
        public int? FPostId { get; set; }
        public int? FUserId { get; set; }
        public string FJoinTime { get; set; }
        public bool FJoincheck { get; set; }

        public virtual TPost FPost { get; set; }
    }
}
