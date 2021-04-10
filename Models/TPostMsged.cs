using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPostMsged
    {
        public TPostMsged()
        {
            TPostMsgedReports = new HashSet<TPostMsgedReport>();
        }

        public int FPostMsgedId { get; set; }
        public int? FPostId { get; set; }
        public int? FPostMsgId { get; set; }
        public int? FUserId { get; set; }
        public string FMsgedDesc { get; set; }
        public string FMsgedTime { get; set; }
        public string FMsgedModifyTime { get; set; }
        public bool FMsgedModify { get; set; }

        public virtual TPostMsg FPostMsg { get; set; }
        public virtual ICollection<TPostMsgedReport> TPostMsgedReports { get; set; }
    }
}
