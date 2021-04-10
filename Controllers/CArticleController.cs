using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;
using Newtonsoft.Json;
using new_layout_core.ViewModel;
using X.PagedList;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace new_layout_core.Controllers
{
    public class CArticleController : Controller
    {
        public int userid = 1;
        WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
        private IHostingEnvironment iv_host;

        public CArticleController(IHostingEnvironment p)
        {
            iv_host = p;
        }
        public IActionResult List()
        {
            return View();
        }

        public JsonResult AllCategory()
        {
            var q = from c in db.TArticleCategories
                    select c;
            List<TArticleCategory> result = null;
            if (q != null)
            {
                result = q.ToList();
            }
            else
            {
                result = null;
            }
            return Json(result);

        }

        public IActionResult search(string keywords)
        {
            if (!String.IsNullOrEmpty(keywords))
            {
                var q = from a in db.TArticles
                        where a.ArtTitle.Contains(keywords)
                        orderby a.ArticleId descending
                        select a;
                List<TArticle> list = q.ToList();
                return PartialView("_Test", list);
            }
            return PartialView("_Test", null);
        }
        public IActionResult catergoryselect(int? cat)
        {
            var q = from a in db.TArticles
                    where a.ArticleCategory.ArticleCategoryId == cat
                    orderby a.ArticleId descending
                    select a;
            List<TArticle> list = q.ToList();
            return PartialView("_Test", list);
        }
        public IActionResult selectarticle()
        {
            var q = from a in db.TArticles
                    orderby a.ArticleId descending
                    select a;
            List<TArticle> list = q.ToList();
            return PartialView("_Test", list);
        }
        public IActionResult selectarticle_Test(int? catid, string? keyword, int? page)
        {
            int pagenumber = page ?? 1;
            int pageSize = 6;
            var articles = from s in db.TArticles
                           where (catid != null ? s.ArticleCategoryId == catid : true) &&
                                 (keyword != null ? s.ArtTitle.Contains(keyword) : true)
                           orderby s.ArticleId descending
                           select s;
            ViewData["pagecount"] = articles.Count();
            ViewData["pagecount"] = articles.Count();
            return PartialView("_Test", articles.ToPagedList(pagenumber, pageSize));
        }

        public JsonResult newestArticle()
        {
            var q = (from a in db.TArticles
                     orderby a.ArticleId descending
                     select new { articleId = a.ArticleId, artTitle = a.ArtTitle, userName = a.User.FUserName, articleImage = a.Image }).Take(3);
            if (q.ToList().Count == 0)
            {
                return Json(null);
            }
            else
            {
                return Json(q);
            }

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(articleCreate_ViewModels a)
        {
            string photoName = Guid.NewGuid().ToString() + ".jpg";
            using (var photo = new FileStream(iv_host.ContentRootPath + @"\wwwroot\imgs\" + photoName,
                FileMode.Create))
            {
                a.photo.CopyTo(photo);
            }
            a.Image = "/imgs/" + photoName;

            db.Add(a.article);
            db.SaveChanges();
            return Json(a.ArticleId);
        }


        public IActionResult ArticleDetail(int id)
        {
            var q = db.TArticles.Where(n => n.ArticleId == id).FirstOrDefault();
            return View(q);
        }
        public JsonResult getContent(int artid)
        {
            var q = db.TArticles.Where(n => n.ArticleId == artid).Select(n => new { art = n, username = n.User.FUserName,userphoto=n.User.FUserPhoto });
            return Json(q);
        }

        public JsonResult userOtherArticle(int userid)
        {
            var q = (from a in db.TArticles
                     where a.UserId == userid
                     orderby a.ArticleId descending
                     select new { articleId = a.ArticleId, artTitle = a.ArtTitle, userName = a.User.FUserName, articleImage = a.Image }).Take(3);
            if (q.ToList().Count == 0)
            {
                return Json(null);
            }
            else
            {
                return Json(q);
            }
        }

        public List<TArticle> SkipAndTake(int page,List<TArticle> articles)
        {
            var q = articles.Skip(page * 6).Take(6);
            return q.ToList();
        }




        public IActionResult ArticleManagement()
        {
            return View();
        }
        public IActionResult OwnersAllArticles(int userid)
        {
            var q = from a in db.TArticles
                     where a.UserId == userid
                     orderby a.ArticleId descending
                     select a;
            List<TArticle> result = null;
            if (q.ToList().Count == 0)
            {
                result = null;
            }
            else
            {
                result = q.ToList();
            }
            
            return PartialView("pv_OwnersArticles", result);
        }
        public IActionResult ArticleEditorPage(int articleid)
        {
            var q = (from a in db.TArticles
                    where a.ArticleId == articleid
                    select a).FirstOrDefault();
            return View(q);
        }
        public JsonResult ArticleEditor(articleEditoe_ViewModels a)
        {
            try
            {
                var q = (from i in db.TArticles
                         where i.ArticleId == a.articleid
                         select i).FirstOrDefault();
                if (q != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    using (var photo = new FileStream(iv_host.ContentRootPath + @"\wwwroot\imgs\" + photoName,
                        FileMode.Create))
                    {
                        a.photo.CopyTo(photo);
                    }
                    a.Image = "/imgs/" + photoName;

                    q.Image = a.Image;
                    q.ArtTitle = a.ArtTitle;
                    q.ArticleCategoryId = a.ArticleCategoryId;
                    q.ArtContent = a.ArtContent;
                    db.SaveChanges();
                    return Json(new { status = "success", articleid = q.ArticleId });
                }
                else
                {
                    return Json(new { status = "fail", why = "欄位錯誤" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "fail", why = ex.Message });
            }

        }

        public IActionResult ArticleDelete(int articleid)
        {
            string message="";
            string status = "";
            try
            {
                
                var report = (from i in db.TArticleReports
                              where i.ArticleId == articleid
                              select i).ToList();
                var reportcheck = (from r in db.TArticleReportChecks
                                   join s in db.TArticleReports on r.ArticleReportId equals s.ArticleReportId
                                   where s.ArticleId == articleid
                                   select r).ToList();

                var reportson = (from m in db.TArticleReports
                                 join s in db.TArticleReportSons on m.ArticleReportId equals s.ArticleReportId
                                 where m.ArticleId == articleid
                                 select s).ToList();

                var reportsoncheck = (from r in db.TArticleReportSonChecks
                                      join s in db.TArticleReportSons on r.ArticleReportSonId equals s.ArticleReportSonId
                                      join m in db.TArticleReports on s.ArticleReportId equals m.ArticleReportId
                                      where m.ArticleId == articleid
                                      select r).ToList();


                var like = (from l in db.TArticleLikes
                            where l.ArticleId == articleid
                            select l).ToList();

                var store = (from l in db.TArticleStores
                            where l.ArticleId == articleid
                            select l).ToList();

                var check = (from l in db.TArticleChecks
                             where l.ArticleId == articleid
                             select l).ToList();
                var tags= (from l in db.TArticleTags
                           where l.ArticleId == articleid
                           select l).ToList();


                if (report.Count != 0 || reportcheck.Count != 0 || reportsoncheck.Count != 0 || reportson.Count != 0 || like.Count != 0 || store.Count != 0 || check.Count != 0 || tags.Count != 0) 
                {
                    //刪TAG
                    if(tags.Count != 0)
                    {
                        foreach (var item in tags)
                        {
                            db.TArticleTags.Remove(item);
                            
                        }
                    }

                    if (reportsoncheck.Count != 0)
                    {
                        foreach (var item in reportsoncheck)
                        {
                            //刪子留言檢舉
                            db.TArticleReportSonChecks.Remove(item);
                        }
                    }

                    if (reportcheck.Count != 0)
                    {
                        foreach(var item in reportcheck)
                        {
                            //刪主留言檢舉
                            db.TArticleReportChecks.Remove(item);
                        }
                    }

                    if (reportson.Count != 0)
                    {
                        foreach (var item in reportson)
                        {
                            //刪子留言
                            db.TArticleReportSons.Remove(item);
                        }
                        foreach (var item in report)
                        {
                            //刪主留言
                            db.TArticleReports.Remove(item);
                        }
                    }
                    else if (report.Count != 0)
                    {
                        foreach (var item in report)
                        {
                            //刪主留言
                            db.TArticleReports.Remove(item);
                        }
                    }
                    if (like.Count != 0)
                    {
                        foreach (var item in like)
                        {
                            //刪喜歡文章
                            db.TArticleLikes.Remove(item);
                        }
                    }
                    if (store.Count != 0)
                    {
                        foreach (var item in store)
                        {
                            //刪收藏文章
                            db.TArticleStores.Remove(item);
                        }
                    }
                    if (check.Count != 0)
                    {
                        foreach (var item in check)
                        {
                            //刪檢舉文章
                            db.TArticleChecks.Remove(item);
                        }
                    }
                }
                var q = db.TArticles.Where(n => n.ArticleId == articleid).FirstOrDefault();
                db.TArticles.Remove(q);
                db.SaveChanges();
                status = "success";
                message = "Delete Success.";

            }
            catch (Exception ex)
            {
                status = "fail";
                message = ex.Message;
            }
            return RedirectToAction("ArticleManagement");
        }
    }
}
