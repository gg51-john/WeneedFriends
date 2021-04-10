using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPostReport
    {
        public int FPostReportId { get; set; }
        public int? FPostId { get; set; }
        public int? FUserId { get; set; }
        public string FPostReportDesc { get; set; }

        public virtual TPost FPost { get; set; }
    }
}
