using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPostMsgedReport
    {
        public int FPostMsgedReportId { get; set; }
        public int? FPostMsgedId { get; set; }
        public int? FUserId { get; set; }
        public string FMsgedReportDesc { get; set; }

        public virtual TPostMsged FPostMsged { get; set; }
    }
}
