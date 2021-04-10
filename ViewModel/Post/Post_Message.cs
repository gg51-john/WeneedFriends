using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_layout_core.ViewModel.Post
{
    public class Post_Message
    {
        public int FPostMsgId { get; set; }
        public int? FPostId { get; set; }
        public int? FUserId { get; set; }
        public string FMsgDesc { get; set; }
        public int? FStart { get; set; }
        public string FMsgTime { get; set; }
        public string FMsgModifyTime { get; set; }
        public bool? FMsgModify { get; set; }
        public string FUserName { get; set; }
        public string FUserPhoto { get; set; }
    }
}
