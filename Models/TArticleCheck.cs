using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TArticleCheck
    {
        public int CheckReportId { get; set; }
        public int? UserId { get; set; }
        public int? ArticleId { get; set; }

        public virtual TArticle Article { get; set; }
        public virtual Member User { get; set; }
    }
}
