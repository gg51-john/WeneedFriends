using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TArticleCategory
    {
        public TArticleCategory()
        {
            TArticles = new HashSet<TArticle>();
        }

        public int ArticleCategoryId { get; set; }
        public string ArticleCategoryName { get; set; }

        public virtual ICollection<TArticle> TArticles { get; set; }
    }
}
