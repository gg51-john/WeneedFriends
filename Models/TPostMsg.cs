using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPostMsg
    {
        public TPostMsg()
        {
            TPostMsgReports = new HashSet<TPostMsgReport>();
            TPostMsgeds = new HashSet<TPostMsged>();
        }

        public int FPostMsgId { get; set; }
        public int? FPostId { get; set; }
        public int? FUserId { get; set; }
        public string FMsgDesc { get; set; }
        public int? FStart { get; set; }
        public string FMsgTime { get; set; }
        public string FMsgModifyTime { get; set; }
        public bool? FMsgModify { get; set; }

        public virtual TPost FPost { get; set; }
        public virtual ICollection<TPostMsgReport> TPostMsgReports { get; set; }
        public virtual ICollection<TPostMsged> TPostMsgeds { get; set; }
    }
}
