using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPostCategory
    {
        public int FPostCategoryId { get; set; }
        public int? FPostId { get; set; }
        public string FCategoryName { get; set; }

        public virtual TPost FPost { get; set; }
    }
}
