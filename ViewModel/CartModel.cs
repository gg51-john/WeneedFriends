using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;

namespace new_layout_core.ViewModel
{
    public class CartModel
    {
        //商品編號
        public int productId { get; set; }
        //商品價格
        public int productPrice { get; set; }
        //商品數量
        public int productQty { get; set; }
        //商品名稱
        public string productName { get; set; }
        //商品小計
        public decimal Amount
        {
            get
            {
                return this.productPrice * this.productQty;
            }
        }
        //商品圖片
        public string Pics { get; set; }

    }
}
