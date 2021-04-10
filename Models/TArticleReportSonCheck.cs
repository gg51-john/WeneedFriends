using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TArticleReportSonCheck
    {
        public int ReportSonCheckId { get; set; }
        public int ArticleReportSonId { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }

        public virtual TArticleReportSon ArticleReportSon { get; set; }
        public virtual Member User { get; set; }
    }
}
