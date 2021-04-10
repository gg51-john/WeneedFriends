using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class TProduct
    {
        public int FProductId { get; set; }
        public string FProductName { get; set; }
        public int? FProductQty { get; set; }
        public int? FProductPrice { get; set; }
        public string FProductCategory { get; set; }
        public string FProductDescription { get; set; }
        public string FSize { get; set; }
    }
}
