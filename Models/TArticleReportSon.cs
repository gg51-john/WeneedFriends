using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TArticleReportSon
    {
        public TArticleReportSon()
        {
            TArticleReportSonChecks = new HashSet<TArticleReportSonCheck>();
        }

        public int ArticleReportSonId { get; set; }
        public int ArticleReportId { get; set; }
        public int? UserId { get; set; }
        public string ReportTime { get; set; }
        public string ReportContent { get; set; }

        public virtual TArticleReport ArticleReport { get; set; }
        public virtual Member User { get; set; }
        public virtual ICollection<TArticleReportSonCheck> TArticleReportSonChecks { get; set; }
    }
}
