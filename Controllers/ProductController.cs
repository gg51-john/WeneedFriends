using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using new_layout_core.Models;
using new_layout_core.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test.ViewModels;

namespace Test.Controllers
{
    public class ProductController : Controller
    {
        private IHostingEnvironment iv_host;
        public ProductController(IHostingEnvironment iv)
        {
            iv_host = iv;
        }
        WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
        public IActionResult ShpList(string seleceName)//    seleceName取篩選的值
        {
            IEnumerable<tProductViewModel> Products = null;
            Products = db.TProducts.ToList().GroupJoin(db.TProductPics, p => p.FProductId, ph => ph.FProductId, (p, pho)=> new tProductViewModel
            {
                FProductId = p.FProductId,
                FProductName = p.FProductName,
                FProductPrice = p.FProductPrice,
                FProductDescription = p.FProductDescription,
                FProductQty = p.FProductQty,
                FProductCategoryId = p.FProductCategory,
                Pics = pho,
                Size=p.FSize
            });
            return View(Products);
        }
        //篩選類型
        public IActionResult select(string selectName)
        {
            IEnumerable<tProductViewModel> Products = null;
            Products = db.TProducts.ToList().Where(n => n.FProductCategory == selectName).GroupJoin(db.TProductPics, p => p.FProductId, ph => ph.FProductId, (p, pho) => new tProductViewModel
            {
                FProductId = p.FProductId,
                FProductName = p.FProductName,
                FProductCategoryId = p.FProductCategory,
                FProductPrice=p.FProductPrice,
                Pics = pho
            });

            return PartialView("ShpListajax", Products);
        }
        //篩選Size
        public IActionResult sizeselect(string selectsize)
        {
            IEnumerable<tProductViewModel> Products = null;
            Products = db.TProducts.ToList().Where(n => n.FSize == selectsize).GroupJoin(db.TProductPics, p => p.FProductId, ph => ph.FProductId, (p, pho) => new tProductViewModel
            {
                FProductId = p.FProductId,
                FProductName = p.FProductName,
                FProductPrice=p.FProductPrice,
                Size =p.FSize,
                Pics = pho
            });

            return PartialView("ShpListajax", Products);
        }
        //後臺管理
        public IActionResult ShpManagement()
        {
            var Products = db.TProducts.ToList().GroupJoin(db.TProductPics, p => p.FProductId, ph => ph.FProductId, (p, pho) => new tProductViewModel
            {
                FProductId = p.FProductId,
                FProductName = p.FProductName,
                FProductPrice = p.FProductPrice,
                FProductDescription = p.FProductDescription,
                FProductQty = p.FProductQty,
                FProductCategoryId = p.FProductCategory,
                Pics = pho,
                Size =p.FSize
            });
            
            
            return View(Products);
        }
        #region 新增產品
        public IActionResult ShpAddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ShpAddProduct(tProductViewModel p)
        {
            db.TProducts.Add(p.product);
            db.SaveChanges();
            foreach (var a in p.imgs)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                using (var photo = new FileStream(iv_host.ContentRootPath + @"\wwwroot\img\" + photoName, FileMode.Create))
                {
                    a.CopyTo(photo);
                }
                TProductPic tp = new TProductPic();
                tp.FProductPicPath = @"/img/" + photoName;
                tp.FProductId = p.product.FProductId;
                db.TProductPics.Add(tp);
                db.SaveChanges();
            }
          
            return RedirectToAction("ShpManagement");
        }
        #endregion
        #region 修改產品
        public IActionResult EditProduct1(int? id)
        {
            WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
            var Products = db.TProducts.ToList().GroupJoin(db.TProductPics, p => p.FProductId, ph => ph.FProductId, (p, pho) => new tProductViewModel
            {
                FProductId = p.FProductId,
                FProductName = p.FProductName,
                FProductPrice = p.FProductPrice,
                FProductDescription = p.FProductDescription,
                FProductQty = p.FProductQty,
                FProductCategoryId = p.FProductCategory,
                Pics = pho,
                Size =p.FSize
            }).FirstOrDefault(n => n.FProductId == id);
            return View(Products);
        }
        [HttpPost]
        public IActionResult EditProduct1(tProductViewModel tProductEdit)
        {
            TProduct EditP = db.TProducts.FirstOrDefault(p => p.FProductId == tProductEdit.FProductId);
            if (EditP != null)
            {
                if (tProductEdit.imgs != null)
                {
                    foreach (var a in tProductEdit.imgs)
                    {
                        string photoName = Guid.NewGuid().ToString() + ".jpg";
                        using (var photo = new FileStream(iv_host.ContentRootPath + @"\wwwroot\img\" + photoName, FileMode.Create))
                        {
                            a.CopyTo(photo);
                        }
                        TProductPic tp = new TProductPic();
                        tp.FProductId = tProductEdit.FProductId;
                        tp.FProductPicPath = @"/img/" + photoName;
                        db.Update(tp);
                        db.SaveChanges();
                    }
                }
                    EditP.FProductName = tProductEdit.FProductName;
                    EditP.FProductPrice = tProductEdit.FProductPrice;
                    EditP.FProductQty = tProductEdit.FProductQty;
                    EditP.FProductDescription = tProductEdit.FProductDescription;
                    EditP.FProductCategory = tProductEdit.FProductCategoryId;
                EditP.FSize = tProductEdit.Size;
                    db.SaveChanges();
                
            }
            return RedirectToAction("ShpManagement");
        }
        #endregion
        //刪除
        public IActionResult DeleteProduct(int? id)
        {
            TProduct tp = db.TProducts.FirstOrDefault(p => p.FProductId == id);
            if (id != null)
            {
                db.TProducts.Remove(tp);
                db.SaveChanges();
            }
            return RedirectToAction("ShpManagement");
        }
        tProductViewModel Products;
        //產品明細
        public IActionResult ShpSingle(int? id)
        {
            Products = db.TProducts.ToList().GroupJoin(db.TProductPics, p => p.FProductId, ph => ph.FProductId, (p, pho) => new tProductViewModel
            {
                FProductId = p.FProductId,
                FProductName = p.FProductName,
                FProductPrice = (int)p.FProductPrice,
                FProductDescription = p.FProductDescription,
                FProductQty = p.FProductQty,
                //FProductCategoryId = p.FProductCategoryId,
                Pics = pho,
            }).FirstOrDefault(n => n.FProductId == id);
            HttpContext.Session.SetObject("text", Products);
            return View(Products);
        }
        List<CartModel> tCart;
        //Ajax加入購物車
        public IActionResult ShpAddToCart(int? id)
        {
            //判斷會員有沒有購物車
            if (HttpContext.Session.GetString("value") == null)
            {
                tCart = new List<CartModel>();
                ShpAddProductToCart(id, null);
            }
            else
            {
                takeCartSession();
                if (tCart.Any(n => n.productId == id))
                {
                    ShpCartAdd(id, null);
                }
                else
                {
                    ShpAddProductToCart(id, null);
                }
            }
            return View();
        }
        //購物車加入方法
        private void ShpAddProductToCart(int? id, int? qty)
        {
            //拿商品詳細訊息
            var product = db.TProducts.Join(db.TProductPics, s => s.FProductId, p => p.FProductId, (s, p) => new
            {
                s.FProductId,
                s.FProductName,
                s.FProductPrice,
                p.FProductPicPath
            }).FirstOrDefault(n => n.FProductId == id);

            //產生一個購物車物件
            tCart.Add(new CartModel
            {
                productId = product.FProductId,
                productName = product.FProductName,
                productPrice = (int)product.FProductPrice,
                productQty = (int)((qty == null) ? 1 : qty),
                Pics = product.FProductPicPath
            });
            //存入session
            HttpContext.Session.SetObject("value", tCart);
        }
        //購物車頁面
        public IActionResult ShpCart()
        {
            return View();
        }
        //購物車頁面資料載入
        public JsonResult take()
        {
            return Json(HttpContext.Session.GetObject<List<CartModel>>("value"));
        }
        //購物車增加商品數量
        public IActionResult ShpCartAdd(int? id, int? qty)
        {
            takeCartSession();
            tCart.Find(n => n.productId == id).productQty += (int)((qty == null) ? 1 : qty);
            HttpContext.Session.SetObject("value", tCart);
            return Json(tCart.Find(n => n.productId == id));
        }
        //購物車總金額
        public IActionResult ShpTotal()
        {
            takeCartSession();
            if (tCart != null)
                return Json(tCart.Sum(n => n.Amount));
            else
                return View();
        }
        //拿session資料
        private void takeCartSession()
        {
            tCart = HttpContext.Session.GetObject<List<CartModel>>("value");
        }
        //結帳後訂單存入資料庫
        public IActionResult ShpPay()
        {
            takeCartSession();

            //訂單紀錄
            List<TmyBuyDetail> detail = new List<TmyBuyDetail>();
            foreach (CartModel t in tCart)
            {
                detail.Add(new TmyBuyDetail
                {
                    FBuytime = DateTime.Now.ToString("yyyy/MM/dd"),
                    FProductId = t.productId,
                    FProductName = t.productName,
                    FProductPrice = t.productPrice,
                    FProductQty = t.productQty,
                    FProductTotal = (int?)t.Amount,
                    FState = "已付款",
                    FUserId = 1084
                });
            }
            db.TmyBuyDetails.AddRange(detail);
            db.SaveChanges();
            HttpContext.Session.Remove("value");
            return RedirectToAction("ShpList");
        }
        //商品細節下方商品表 隨機或固定四筆
        public JsonResult ShpListTable()
        {
            IEnumerable<tProductViewModel> Products = db.TProducts.ToList().GroupJoin(db.TProductPics, p => p.FProductId, ph => ph.FProductId, (p, pho) => new tProductViewModel
            {
                FProductId = p.FProductId,
                FProductName = p.FProductName,
                FProductPrice = (int)p.FProductPrice,
                Pics = pho.Take(1)
            }).TakeLast(4);
            //var pageCnt = 1;
            //var pageRows = 4;
            //var query = Products.Skip(((pageCnt - 1) * pageRows)).Take(pageRows);
            return Json(JsonConvert.SerializeObject(Products));
        }
        public JsonResult ShpImg(int? id)
        {
            IEnumerable<tProductViewModel> tProducts = db.TProducts.ToList().GroupJoin(db.TProductPics, p => p.FProductId, ph => ph.FProductId, (p, pho) => new tProductViewModel
            {
                FProductId = p.FProductId,
                Pics = pho.ToList().Where(n=>n.FProductId==48)
            }) ;
            return Json(JsonConvert.SerializeObject(tProducts));
        }
        //個人訂單紀錄
        public IActionResult ShpRecord(int? id)
        {
            var s = db.TmyBuyDetails.Where(s => s.FUserId == 1084).Select(n => n);
            //return Json(db.TMyBuyDetails.Where(n => n.FUserId == 4));
            return View(s);
        }
        public IActionResult ShpPersonalRecord()
        {
            return View();
        }
        //商品詳細頁面 選定數量加入購物車
        public JsonResult text2(int id, int qty)
        {
            if (HttpContext.Session.GetString("value") == null)
            {
                tCart = new List<CartModel>();
                ShpAddProductToCart(id, qty);
            }
            else
            {
                takeCartSession();
                if (tCart.Any(n => n.productId == id))
                {
                    //新增購物車產品數量
                    ShpCartAdd(id, qty);
                }
                else
                {
                    //新增一筆商品至購物車
                    ShpAddProductToCart(id, qty);
                }
            }
            return Json("");
        }
        public IActionResult text3()
        {
            IEnumerable<tProductViewModel> Products = db.TProducts.ToList().GroupJoin(db.TProductPics, p => p.FProductId, ph => ph.FProductId, (p, pho) => new tProductViewModel
            {
                FProductId = p.FProductId,
                FProductName = p.FProductName,
                FProductPrice = (int)p.FProductPrice,
                FProductDescription = p.FProductDescription,
                //FProductQty = p.FProductQty,
                //FProductCategoryId = p.FProductCategoryId,
               Pics = pho
            }).TakeLast(3);
            return Json(JsonConvert.SerializeObject(Products));
        }
        public IActionResult Text()
        {
            return View();
        }
    }


    
}
