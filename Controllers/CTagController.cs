using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;

namespace Test.Controllers
{
    public class CTagController : Controller
    {
        WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
        public IActionResult TagPage(int tagid)
        {
            var thistag = db.TTags.Where(n => n.TagId == tagid).FirstOrDefault();
            return View(thistag);
        }
        public IActionResult GetTagArticles(int tagid)
        {
            var article = from a in db.TArticles join
                          t in db.TArticleTags on a.ArticleId equals t.ArticleId
                          where t.TagId == tagid
                          select a;

            return PartialView("pv_TagArticle",article.ToList());
        }

        public JsonResult getarttags(int artid)
        {
            var q = from t in db.TArticleTags
                    where t.ArticleId == artid
                    select new { tagname = t.Tag.TagName, tagid = t.TagId };
            if (q.ToList().Count == 0)
            {
                return Json(null);
            }
            else
            {
                return Json(q);
            }
        }
        public JsonResult gettags()
        {
            var alltags = from t in db.TTags
                           select t;

            return Json(alltags);
        }
        public JsonResult createTag(string name)
        {
            string result = "";
            string status = "";
            try
            {
                int count = db.TTags.Where(n => n.TagName.Contains(name)).Count();
                if (count != 0)
                {
                    status = "fail";
                    result = "已有相關標籤";
                }
                else
                {
                    TTag tag = new TTag();
                    tag.TagName = name;
                    db.TTags.Add(tag);
                    db.SaveChanges();
                    status = "success";
                    result = "新增標籤成功!";
                }
            }
            catch(Exception ex)
            {
                status = "fail";
                result = "程序錯誤:"+ex.Message;
            }

            return Json(new { status = status, result = result });
        }
        public string savetags(string tagid, int artid)
        {

            TArticleTag at = new TArticleTag();
            if (tagid != null)
            {
                List<tags> a = JsonConvert.DeserializeObject<List<tags>>(tagid);
                foreach (var id in a)
                {
                    at.ArticleId = artid;
                    at.TagId = int.Parse(id.id);
                    at.ArticleTagId = 0;
                    db.TArticleTags.Add(at);
                    db.SaveChanges();
                };
                return "ggbb";
            }
            else
            {
                return "false";
            }
        }
        public JsonResult editortag(string tagid, int artid)
        {
            try
            {
                if (tagid != null)
                {
                    TArticleTag at = new TArticleTag();
                    var oldtags = (from t in db.TArticleTags
                                   where t.ArticleId == artid
                                   select t).ToList();
                    foreach (var item in oldtags)
                    {
                        db.TArticleTags.Remove(item);
                        db.SaveChanges();
                    }

                    List<tags> a = JsonConvert.DeserializeObject<List<tags>>(tagid);
                    foreach (var id in a)
                    {
                        at.ArticleId = artid;
                        at.TagId = int.Parse(id.id);
                        at.ArticleTagId = 0;
                        db.TArticleTags.Add(at);
                        db.SaveChanges();
                    };
                    return Json(new { result = "success", status = "Tags change success." });
                }
                else
                {
                    return Json(new { result = "success", status = "Here is no Tags need to change." }) ;
                }
            }
            catch(Exception ex)
            {
                return Json(new { result = "fail", status = "伺服器錯誤: "+ex.Message }); ;
            }
            
        }
        public class tags
        {
            public string id;
        }

    }
}
