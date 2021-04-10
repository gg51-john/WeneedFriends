using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TArticleReportCheck
    {
        public int UserCheckreportId { get; set; }
        public int? ArticleReportId { get; set; }
        public int? UserId { get; set; }
        public string Reason { get; set; }

        public virtual TArticleReport ArticleReport { get; set; }
    }
}
