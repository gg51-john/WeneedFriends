using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Test.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using new_layout_core.Models;
using new_layout_core.ViewModel;
using System.Text;
using System.Security.Cryptography;

namespace Test.Controllers
{
    public class ShoppingController : Controller
    {
        WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
        List<CartModel> tcart;
        //加入購物車方法

        //Ajax加入購物車
        public IActionResult ShpCart(int? id)
        {
            if (HttpContext.Session.GetString("value") == null)
            {
                tcart = new List<CartModel>();
                ShpAddToCart(id);
            }
            else
            {
                tcart = HttpContext.Session.GetObject<List<CartModel>>("value");
                if (tcart.Any(a => a.productId == id))
                {
                    ShpAddQty(id);
                }
                else
                {
                    ShpAddToCart(id);
                }
            }
            return View();
        }
        public void ShpAddToCart(int? id)
        {
            var products = db.TProducts.Join(db.TProductPics, s => s.FProductId, p => p.FProductId, (s, p) => new
            {
                s.FProductId,
                s.FProductName,
                s.FProductPrice,
                p.FProductPicPath
            }).FirstOrDefault(n => n.FProductId == id);

            tcart.Add(new CartModel
            {
                productId = products.FProductId,
                productName = products.FProductName,
                productPrice = (int)products.FProductPrice,
                productQty = 1,
                Pics = products.FProductPicPath
            });
            HttpContext.Session.SetObject("value", tcart);
        }
        //購物車數量增加
        public IActionResult ShpAddQty(int? id)
        {
            tcart = HttpContext.Session.GetObject<List<CartModel>>("value");
            tcart.Find(n => n.productId == id).productQty += 1;
            HttpContext.Session.SetObject("value", tcart);
            return Json(tcart.Find(n => n.productId == id));
        }
        //購物車數量減少
        public IActionResult ShpDelQty(int? id)
        {
            tcart = HttpContext.Session.GetObject<List<CartModel>>("value");
            tcart.Find(n => n.productId == id).productQty -= 1;
            HttpContext.Session.SetObject("value", tcart);
            return Json(tcart.Find(n => n.productId == id));
        }
        //刪除照片
        public IEnumerable<TProductPic> RemoveImg(string img)
        {
            WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
            TProductPic pic = db.TProductPics.FirstOrDefault(n => n.Fid == int.Parse(img));
            db.TProductPics.Remove(pic);
            db.SaveChanges();
            var Pic2 = db.TProductPics.Select(n => n);
            return Pic2;
        }
        //刪除購物車商品
        public IActionResult ShpCartDelete(int? id)
        {
            tcart = HttpContext.Session.GetObject<List<CartModel>>("value");
            tcart.RemoveAll(item => item.productId == id);
            HttpContext.Session.SetObject("value", tcart);
            return View();
        }
        //購物車總計
        public IActionResult ShpTotal()
        {
            tcart = HttpContext.Session.GetObject<List<CartModel>>("value");
            if (tcart != null)
                return Json(tcart.Sum(m => m.Amount));
            else
                return View();
        }
        //購物車載入畫面
        public JsonResult take()
        {
            var s = HttpContext.Session.GetObject<List<CartModel>>("value");
            return Json(HttpContext.Session.GetObject<List<CartModel>>("value"));
        }
        public IActionResult ShpCartd()
        {
            return Json(HttpContext.Session.GetObject<List<CartModel>>("value"));
        }
        public IActionResult ShpCartView()
        {
            return View();
        }
        public JsonResult OPay()
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            int passwordLength = 4;
            char[] chars = new char[passwordLength];
            Random rd = new Random();
            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            string password = new string(chars);
            int opend = 2;
            char[] open = new char[opend];
            for (int a = 0; a < opend; a++)
            {
                open[a] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            string opendd = new string(open);
            //拿購物車
            tcart = HttpContext.Session.GetObject<List<CartModel>>("value");
            //亂數
            string itemname = "";
            int amount = 0;
            if (tcart != null)
            {
                foreach (var a in tcart)
                {
                    itemname += a.productName + " " + a.productPrice + "X" + a.productQty + "#";
                    amount += (int)a.Amount;
                }
                List<Send> send1 = new List<Send>();
                Send send = new Send();
                send.itemname = itemname;
                send.amount = amount.ToString();
                send.returnurl = "http://192.168.36.41/Product/ShpList";
                send.succesreturnurl = "http://192.168.36.41/Product/ShpList";
                send.time = opendd + DateTime.Now.ToString("yyyyMMddhhmmss") + password;
                send.time3 = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                send.hashkey = "5294y06JbISpM5x9";
                send.hashiv = "v77hoKGq4kWxNNIS";
                var input = $"HashKey=5294y06JbISpM5x9&ChoosePayment=Credit&ClientBackURL={send.returnurl}&CreditInstallment=&EncryptType=1&InstallmentAmount=&ItemName={send.itemname}&MerchantID=2000132&MerchantTradeDate={send.time3}&MerchantTradeNo={send.time}&PaymentType=aio&Redeem=&ReturnURL={send.succesreturnurl}&StoreID=&TotalAmount={send.amount}&TradeDesc=建立信用卡測試訂單&HashIV=v77hoKGq4kWxNNIS";
                string encoded = System.Web.HttpUtility.UrlEncode(input).ToLower();
                byte[] messageBytes = Encoding.Default.GetBytes(encoded);
                SHA256 sHA256 = new SHA256CryptoServiceProvider();
                byte[] vs = sHA256.ComputeHash(messageBytes);
                string result = BitConverter.ToString(vs).ToUpper();
                result = result.Replace("-", "");
                send.checkmacvalue = result;
                send1.Add(send);
                return Json(new { JsonResult = send1 });
            }
            else
            {
                return Json("");
            }
        }
        



    }

}

