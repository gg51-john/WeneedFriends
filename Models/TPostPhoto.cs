using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPostPhoto
    {
        public int FPostPhotoId { get; set; }
        public int? FPostId { get; set; }
        public string FPostPhoto { get; set; }

        public virtual TPost FPost { get; set; }
    }
}
