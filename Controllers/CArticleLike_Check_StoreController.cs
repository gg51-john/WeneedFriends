using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;

namespace Test.Controllers
{
    public class CArticleLike_Check_StoreController : Controller
    {
        WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();

        //---------------------------------檢舉開始---------------------------------
        public JsonResult getcheck(int articleid)
        {
            int count = 0;
            var q = from l in db.TArticleChecks
                    where l.ArticleId == articleid
                    select l;
            count = q.Count();
            return Json(new { result = count });
        }
        public JsonResult checkchecks(int articleid, int userid)
        {
            var result = false;
            try
            {
                if (articleid != 0 && userid != 0)
                {
                    var q = from l in db.TArticleChecks
                            where l.ArticleId == articleid && l.UserId == userid
                            select l;
                    if (q.ToList().Count == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return Json(new { result = result });
            }
            catch
            {
                return Json(new { result = result });
            }
        }


        public JsonResult removecheck(int articleid, int userid)
        {
            var result = false;
            try
            {
                var q = (from l in db.TArticleChecks
                         where l.ArticleId == articleid && l.UserId == userid
                         select l).FirstOrDefault();
                db.TArticleChecks.Remove(q);
                db.SaveChanges();
                result = true;
                return Json(new { result = result });
            }
            catch
            {
                result = false;
                return Json(new { result = result });
            }

        }
        public JsonResult addcheck(int articleid, int userid)
        {
            var result = false;
            try
            {
                TArticleCheck check = new TArticleCheck();
                check.ArticleId = articleid;
                check.UserId = userid;
                db.TArticleChecks.Add(check);
                db.SaveChanges();
                result = true;
                return Json(new { result = result });
            }
            catch
            {
                result = false;
                return Json(new { result = result });
            }

        }
        //---------------------------------檢舉結束---------------------------------
        //---------------------------------喜歡開始---------------------------------
        public JsonResult checkheart(int articleid, int userid)
        {
            var result = false;
            try
            {
                if (articleid != 0 && userid != 0)
                {
                    var q = from l in db.TArticleLikes
                            where l.ArticleId == articleid && l.UserId == userid
                            select l;
                    if (q.ToList().Count == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return Json(new { result = result });
            }
            catch
            {
                return Json(new { result = result });
            }
        }
        public JsonResult getlikecounts(int articleid)
        {
            int count = 0;
            var q = from l in db.TArticleLikes
                    where l.ArticleId == articleid
                    select l;
            count = q.Count();
            return Json(new { result = count });
        }

        public JsonResult removelike(int articleid, int userid)
        {
            var result = false;
            try
            {
                var q = (from l in db.TArticleLikes
                         where l.ArticleId == articleid && l.UserId == userid
                         select l).FirstOrDefault();
                db.TArticleLikes.Remove(q);
                db.SaveChanges();
                result = true;
                return Json(new { result = result });
            }
            catch
            {
                result = false;
                return Json(new { result = result });
            }

        }
        public JsonResult addlike(int articleid, int userid)
        {
            var result = false;
            try
            {
                TArticleLike like = new TArticleLike();
                like.ArticleId = articleid;
                like.UserId = userid;
                db.TArticleLikes.Add(like);
                db.SaveChanges();
                result = true;
                return Json(new { result = result });
            }
            catch
            {
                result = false;
                return Json(new { result = result });
            }

        }
        //---------------------------------喜歡結束---------------------------------
        //---------------------------------收藏開始---------------------------------
        public IActionResult GetMyStores(int userid)
        {
            var q = from l in db.TArticles join
                    s in db.TArticleStores on l.ArticleId equals s.ArticleId
                    where s.UserId == userid
                    select l;
            return PartialView("pv_mystore", q.ToList());
        }
        public JsonResult getstore(int articleid)
        {
            int count = 0;
            var q = from l in db.TArticleStores
                    where l.ArticleId == articleid
                    select l;
            count = q.Count();
            return Json(new { result = count });
        }
        public JsonResult checkstore(int articleid, int userid)
        {
            var result = false;
            try
            {
                if (articleid != 0 && userid != 0)
                {
                    var q = from l in db.TArticleStores
                            where l.ArticleId == articleid && l.UserId == userid
                            select l;
                    if (q.ToList().Count == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return Json(new { result = result });
            }
            catch
            {
                return Json(new { result = result });
            }
        }


        public JsonResult removestore(int articleid, int userid)
        {
            var result = false;
            try
            {
                var q = (from l in db.TArticleStores
                         where l.ArticleId == articleid && l.UserId == userid
                         select l).FirstOrDefault();
                db.TArticleStores.Remove(q);
                db.SaveChanges();
                result = true;
                return Json(new { result = result });
            }
            catch
            {
                result = false;
                return Json(new { result = result });
            }

        }
        public JsonResult addstore(int articleid, int userid)
        {
            var result = false;
            try
            {
                TArticleStore check = new TArticleStore();
                check.ArticleId = articleid;
                check.UserId = userid;
                db.TArticleStores.Add(check);
                db.SaveChanges();
                result = true;
                return Json(new { result = result });
            }
            catch
            {
                result = false;
                return Json(new { result = result });
            }

        }
        //---------------------------------收藏結束---------------------------------
        //---------------------------------留言檢舉開始---------------------------------
        public JsonResult msgcheck(int msgid, int userid,string reson)
        {
            try
            {
                string msg = "";
                string status = "";
                var q = db.TArticleReportChecks.Where(n => n.UserId == userid && n.ArticleReportId==msgid).ToList().Count;
                if (q == 0)
                {
                    TArticleReportCheck check = new TArticleReportCheck();
                    check.ArticleReportId = msgid;
                    check.UserId = userid;
                    check.Reason = reson;
                    db.TArticleReportChecks.Add(check);
                    db.SaveChanges();
                    msg = "success";
                    status = "success";
                }
                else
                {
                    status = "fail";
                    msg = "已經檢舉過囉!";
                }
                return Json(new { status= status, result= msg }) ;
            } catch (Exception ex)
            {

                return Json(new { status = "fail", result = ex.Message });
            }
            
        }
        public JsonResult msgsoncheck(int msgid, int userid, string reson)
        {
            try
            {
                string msg = "";
                string status = "";
                var q = db.TArticleReportSonChecks.Where(n => n.UserId == userid && n.ArticleReportSonId == msgid).ToList().Count;
                if (q == 0)
                {
                    TArticleReportSonCheck check = new TArticleReportSonCheck();
                    check.ArticleReportSonId = msgid;
                    check.UserId = userid;
                    check.Reason = reson;
                    db.TArticleReportSonChecks.Add(check);
                    db.SaveChanges();
                    msg = "success";
                    status = "success";
                }
                else
                {
                    status = "fail";
                    msg = "已經檢舉過囉!";
                }
                return Json(new { status = status, result = msg });
            }
            catch (Exception ex)
            {

                return Json(new { status = "fail", result = ex.Message });
            }

        }

        //---------------------------------留言檢舉結束---------------------------------

    }
}
