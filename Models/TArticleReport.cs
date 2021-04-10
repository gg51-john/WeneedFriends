using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TArticleReport
    {
        public TArticleReport()
        {
            TArticleReportChecks = new HashSet<TArticleReportCheck>();
            TArticleReportSons = new HashSet<TArticleReportSon>();
        }

        public int ArticleReportId { get; set; }
        public int? ArticleId { get; set; }
        public string ArticleReport { get; set; }
        public int? UserId { get; set; }
        public string ArticleReportTime { get; set; }
        public int? MessageLikeCount { get; set; }
        public int? MessageCheck { get; set; }

        public virtual TArticle Article { get; set; }
        public virtual Member User { get; set; }
        public virtual ICollection<TArticleReportCheck> TArticleReportChecks { get; set; }
        public virtual ICollection<TArticleReportSon> TArticleReportSons { get; set; }
    }
}
