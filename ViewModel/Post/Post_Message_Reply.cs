using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_layout_core.ViewModel.Post
{
    public class Post_Message_Reply
    {
        public int FPostMsgedId { get; set; }
        public int? FPostId { get; set; }
        public int? FPostMsgId { get; set; }
        public int? FUserId { get; set; }
        public string FMsgedDesc { get; set; }
        public string FMsgedTime { get; set; }
        public string FMsgedModifyTime { get; set; }
        public bool FMsgedModify { get; set; }
        public string FUserName { get; set; }
        public string FUserPhoto { get; set; }
    }
}
