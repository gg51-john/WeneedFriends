using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TMemberTag
    {
        public int MemberTagId { get; set; }
        public int UserId { get; set; }
        public int TagId { get; set; }

        public virtual TTag Tag { get; set; }
        public virtual Member User { get; set; }
    }
}
