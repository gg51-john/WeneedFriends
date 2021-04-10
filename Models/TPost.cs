using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TPost
    {
        public TPost()
        {
            TJoinPeople = new HashSet<TJoinPerson>();
            TPostLikeCounts = new HashSet<TPostLikeCount>();
            TPostMsgs = new HashSet<TPostMsg>();
            TPostPhotos = new HashSet<TPostPhoto>();
            TPostReports = new HashSet<TPostReport>();
            TPostStores = new HashSet<TPostStore>();
            TPostTags = new HashSet<TPostTag>();
        }

        public int FPostId { get; set; }
        public int FUserId { get; set; }
        public string FTitle { get; set; }
        public string FSportName { get; set; }
        public string FDescription { get; set; }
        public string FPostTime { get; set; }
        public int FPeople { get; set; }
        public string FLikeCount { get; set; }
        public string FSystemTime { get; set; }
        public string FPostCity { get; set; }
        public string FPostDistrict { get; set; }
        public string FPostAddress { get; set; }
        public bool FPostCheck { get; set; }

        public virtual Member FUser { get; set; }
        public virtual ICollection<TJoinPerson> TJoinPeople { get; set; }
        public virtual ICollection<TPostLikeCount> TPostLikeCounts { get; set; }
        public virtual ICollection<TPostMsg> TPostMsgs { get; set; }
        public virtual ICollection<TPostPhoto> TPostPhotos { get; set; }
        public virtual ICollection<TPostReport> TPostReports { get; set; }
        public virtual ICollection<TPostStore> TPostStores { get; set; }
        public virtual ICollection<TPostTag> TPostTags { get; set; }
    }
}
