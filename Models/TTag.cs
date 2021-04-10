using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TTag
    {
        public TTag()
        {
            TArticleTags = new HashSet<TArticleTag>();
            TMemberTags = new HashSet<TMemberTag>();
            TPostTags = new HashSet<TPostTag>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<TArticleTag> TArticleTags { get; set; }
        public virtual ICollection<TMemberTag> TMemberTags { get; set; }
        public virtual ICollection<TPostTag> TPostTags { get; set; }
    }
}
