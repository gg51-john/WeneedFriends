using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TmyBuyDetail
    {
        public int Fid { get; set; }
        public int? FProductId { get; set; }
        public int? FUserId { get; set; }
        public string FBuytime { get; set; }
        public string FProductName { get; set; }
        public int? FProductPrice { get; set; }
        public int? FProductQty { get; set; }
        public int? FProductTotal { get; set; }
        public string FState { get; set; }

        public virtual Member FUser { get; set; }
    }
}
