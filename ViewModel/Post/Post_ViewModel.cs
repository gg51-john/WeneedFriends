using new_layout_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_layout_core.ViewModel.Post
{
    public class Post_ViewModel
    {
        public TPost tpost { get; set; }
        public IEnumerable<TPostPhoto> tpostPhotos { get; set; }
        public IEnumerable<TTag> tpostTags { get; set; }
        public int join_count { get; set; }
    }
}
