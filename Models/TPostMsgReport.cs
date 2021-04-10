using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPostMsgReport
    {
        public int FPostMsgReportId { get; set; }
        public int? FPostMsgId { get; set; }
        public int? FUserId { get; set; }
        public string FMsgReportDesc { get; set; }

        public virtual TPostMsg FPostMsg { get; set; }
    }
}
