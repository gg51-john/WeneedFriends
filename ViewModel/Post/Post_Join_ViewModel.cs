using new_layout_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_layout_core.ViewModel.Post
{
    public class Post_Join_ViewModel
    {
        public TJoinPerson joinPerson { get; set; }
        public Member member { get; set; }
        public int userStatus { get; set; } //0.主辦 1.沒參加  2.參加 3.等待參加
    }
}
