using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace new_layout_core.ViewModel
{
    public class CPost_Edit
    {
        public int FPostId { get; set; }
        public int FUserId { get; set; }
        [DisplayName("活動名稱")]
        public string FTitle { get; set; }
        [DisplayName("活動類型")]
        public string FSportName { get; set; }
    }
}
