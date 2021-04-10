using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TArticle
    {
        public TArticle()
        {
            TArticleChecks = new HashSet<TArticleCheck>();
            TArticleLikes = new HashSet<TArticleLike>();
            TArticleReports = new HashSet<TArticleReport>();
            TArticleStores = new HashSet<TArticleStore>();
            TArticleTags = new HashSet<TArticleTag>();
        }

        public int ArticleId { get; set; }
        public int? ArticleCategoryId { get; set; }
        public int? UserId { get; set; }
        public string ArtTitle { get; set; }
        public string ArtContent { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }

        public virtual TArticleCategory ArticleCategory { get; set; }
        public virtual Member User { get; set; }
        public virtual ICollection<TArticleCheck> TArticleChecks { get; set; }
        public virtual ICollection<TArticleLike> TArticleLikes { get; set; }
        public virtual ICollection<TArticleReport> TArticleReports { get; set; }
        public virtual ICollection<TArticleStore> TArticleStores { get; set; }
        public virtual ICollection<TArticleTag> TArticleTags { get; set; }
    }
}
