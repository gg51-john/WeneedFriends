using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class PostReport
    {
        public int PostReportId { get; set; }
        public int? PostId { get; set; }
        public string PostReport1 { get; set; }
        public int? UserId { get; set; }
        public DateTime ArticleReportTime { get; set; }
        public int? MessageLikeCount { get; set; }
        public int? MessageCheck { get; set; }

        public virtual TPost Post { get; set; }
    }
}
