using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TArticleTag
    {
        public int ArticleTagId { get; set; }
        public int? ArticleId { get; set; }
        public int? TagId { get; set; }

        public virtual TArticle Article { get; set; }
        public virtual TTag Tag { get; set; }
    }
}
