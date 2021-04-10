using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;
namespace new_layout_core.ViewModel
{

    public class tProductViewModel
    {
        //private TProduct selectName { get; set; }

        private TProduct iv_product = null;
        public TProduct product { get { return iv_product; } }
        public tProductViewModel()
        {
            iv_product = new TProduct();
        }
        public tProductViewModel(TProduct p)
        {
            iv_product = p;
        }
        [DisplayName("產品序號")]
        public int FProductId { get { return iv_product.FProductId; } set { iv_product.FProductId = value; } }
        [DisplayName("產品名稱")]
        public string FProductName { get { return iv_product.FProductName; } set { iv_product.FProductName = value; } }
        [DisplayName("產品數量")]
        public int? FProductQty { get { return iv_product.FProductQty; } set { iv_product.FProductQty = value; } }
        [DisplayName("產品單價")]
        public int? FProductPrice { get { return iv_product.FProductPrice; } set { iv_product.FProductPrice = value; } }
        [DisplayName("類型")]
        public string FProductCategoryId { get { return iv_product.FProductCategory; } set { iv_product.FProductCategory= value; } }
        [DisplayName("產品說明")]
        public string FProductDescription { get { return iv_product.FProductDescription; } set { iv_product.FProductDescription = value; } }
        [DisplayName("產品圖片")]
        public IEnumerable<TProductPic>Pics  {get;set;}
        public List <IFormFile> imgs { get; set; }
        //public int? FSize { get { return iv_product.FSize; } set { iv_product.FSize=value; } }

        public string Size { get { return iv_product.FSize; } set { iv_product.FSize = value; } }
       
    }
}
