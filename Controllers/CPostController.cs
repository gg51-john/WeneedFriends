using Microsoft.AspNetCore.Mvc;
using new_layout_core.Models;
using new_layout_core.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.AspNetCore.Http;

namespace weneedfriend.Controllers
{
    public class CPostController : Controller
    {
        private readonly WeNeedFriendsFINContext db;
        
        internal static int pageSize = 2;

        
        public CPostController(WeNeedFriendsFINContext wnf)
        {
            db = wnf;
           
        }
        //todo postlist 頁面
        public IActionResult Post_Index()
        {
            return View();
        }        
        [HttpPost]
        public IActionResult Post_Index_postsearch(string date_in_format, string date_out_format, string city, string district, string sport, int? star,int? page)
        {
            DateTime date_in_Convert = new DateTime();
            DateTime date_out_Convert = new DateTime();
            if (date_in_format !="*") date_in_Convert = Convert.ToDateTime(date_in_format);
            if (date_out_format != "*") date_out_Convert = Convert.ToDateTime(date_out_format);
            if (city == null || city == "-") city = "*";
            if (district == null || district == "-") district = "*";
            if (sport == null || sport == "-") sport = "*";
            if (star == null) star = 0;

            int pageNumber = page ?? 1;
            var query = db.TPosts.AsEnumerable().Where(p => (city != "*" ? p.FPostCity.Contains(city) : true) &&
                                                            (district != "*" ? p.FPostDistrict.Contains(district) : true) &&
                                                            (date_in_format != "*" ? Convert.ToDateTime(p.FPostTime) > date_in_Convert : true) &&
                                                            (date_out_format != "*" ? Convert.ToDateTime(p.FPostTime) < date_out_Convert : true) &&
                                                            (sport != "*" ? p.FSportName.Contains(sport) : true))
                                                .GroupJoin(db.TPostPhotos, p => p.FPostId, ph => ph.FPostId, (p, ph) => (p, ph))
                                                .GroupJoin(db.TPostTags, p1 => p1.p.FPostId, c => c.FPostId, (p1, c) => (p1, c))
                                                .GroupJoin(db.TJoinPeople, p2 => p2.p1.p.FPostId, j => j.FPostId, (p2, j) =>
                                                new Post_ViewModel
                                                {
                                                    tpost = p2.p1.p,
                                                    tpostPhotos = p2.p1.ph,
                                                    tpostTags = p2.c.Join(db.TTags, t => t.FTagId, tn => tn.TagId, (t, tn) => tn),
                                                    join_count = j.Count(),
                                                    
                                                });

            ViewData["pagecount"] = query.Count();
            return PartialView("Post_Index_postlist", query.ToPagedList(pageNumber, pageSize));
        }
        
        //[HttpPost]
        public IActionResult Post_Index_postlist(int? page)
        {

            int pageNumber = page ?? 1;
            var query = db.TPosts.AsEnumerable()
                                .GroupJoin(db.TPostPhotos, p => p.FPostId, ph => ph.FPostId, (p, ph) => (p, ph))
                                .GroupJoin(db.TPostTags, p1 => p1.p.FPostId, c => c.FPostId, (p1, c) => (p1, c))
                                
                                .GroupJoin(db.TJoinPeople, p2 => p2.p1.p.FPostId, j => j.FPostId, (p2, j) =>
                                new Post_ViewModel
                                {
                                    tpost = p2.p1.p,
                                    tpostPhotos = p2.p1.ph,
                                    tpostTags = p2.c.Join(db.TTags, t => t.FTagId, tn => tn.TagId, (t, tn) => tn),
                                    join_count = j.Count(),
                                });
            if(query !=null)ViewData["pagecount"] = query.Count();
            return  PartialView("Post_Index_postlist", query.ToPagedList(pageNumber,pageSize));
        }

        //todo 推薦文章or 活動
        [HttpPost]
        public IActionResult Post_Index_newpost()
        {
            var query = db.TPosts.AsEnumerable().GroupJoin(db.TPostPhotos, p => p.FPostId, ph => ph.FPostId, (p, ph) => (p, ph))
                .OrderByDescending(p => p.p.FSystemTime).Take(3).Select(p => new Post_ViewModel { tpost = p.p, tpostPhotos = p.ph, tpostTags = null, join_count = 0 });
            return PartialView("Post_Index_newpost", query);
        }

        //todo get city select
        [HttpPost]
        public JsonResult cityoption()
        {
            var query = db.TCities;
            return Json(query);
        }

        //todo get district select
        [HttpPost]
        public JsonResult districtoption(string city)
        {
            var query = db.TLocations.Join(db.TCities, l => l.FCityId, c => c.FCityId, (l, c) => new { fCity = c.FCityName, fDistrict = l.FDistrictName }).Where(l => l.fCity == city);
            return Json(query);
        }
        //todo get sport select
        [HttpPost]
        public JsonResult sportoption()
        {
            var query = db.TSports;
            return Json(query);
        }
        //todo get tag select
        [HttpPost]
        public JsonResult tagoption()
        {
            var query = db.TTags;
            return Json(query);
        }
        //todo postdetail 頁面



        public IActionResult Post_Detail(int? id)
        {
            if (id != null)
            {
                var query = db.TPosts.FirstOrDefault(p => p.FPostId == id);
                Post_Detail pd = new Post_Detail { post = query };
                return View(pd);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Post_Detail_post(int? id)
        {
            if (id != null)
            {
                var query = db.TPosts.AsEnumerable().GroupJoin(db.TPostPhotos, p => p.FPostId, ph => ph.FPostId, (p, ph) => (p, ph))
                                                .GroupJoin(db.TPostTags, p1 => p1.p.FPostId, c => c.FPostId, (p1, c) => (p1, c))
                                                .GroupJoin(db.TJoinPeople, p2 => p2.p1.p.FPostId, j => j.FPostId, (p2, j) =>
                                                new Post_ViewModel
                                                {
                                                    tpost = p2.p1.p,
                                                    tpostPhotos = p2.p1.ph,
                                                    tpostTags = p2.c.Join(db.TTags, t => t.FTagId, tn => tn.TagId, (t, tn) => tn),
                                                    join_count = j.Count()
                                                }).FirstOrDefault(p=>p.tpost.FPostId==id);

                return PartialView("Post_Detail_post", query);
            }
            return View("Post_Index");
        }
        [HttpPost]
        public IActionResult Post_Detail_people(int? id)
        {

            PeopleCount query = db.TPosts.AsEnumerable()
                                         .GroupJoin(db.TJoinPeople, p => p.FPostId, j => j.FPostId, (p, j) => new PeopleCount
                                         {
                                             PostID=p.FPostId,
                                             PeopleTotal = p.FPeople,
                                             joined = j.Count(p => p.FJoincheck == true),
                                             waited = j.Count(p => p.FJoincheck == false),
                                         }).FirstOrDefault(p => p.PostID == id);
            return PartialView("Post_Detail_people", query);
        }



        private IQueryable<Post_Join_ViewModel> joinlist(int? id)
        {

            var query = db.TJoinPeople.Join(db.Members, j => j.FUserId, m => m.FUserId, (j, m) => new Post_Join_ViewModel
            {
                joinPerson = j,
                member = m,
                userStatus=0
            }).Where(p => p.joinPerson.FPostId == id).OrderBy(p => p.joinPerson.FJoinId);
            return query;
        }

        [HttpPost]
        public IActionResult Post_Detail_Leader(int? id)
        {
            if (id != null)
            {
                var query = joinlist(id).FirstOrDefault();               
                return PartialView("Post_Detail_Leader", query);                
            }
            return View("Post_Index");
        }
        [HttpPost]
        public IActionResult Post_Detail_join(int? id)
        {
            if (id != null)
            {
                int people = db.TPosts.FirstOrDefault(p => p.FPostId == id).FPeople;
                IQueryable<Post_Join_ViewModel> query = joinlist(id).Skip(1);
                return PartialView("Post_Detail_join", query);
            }
            return View("Post_Index");
        }

        [HttpPost]
        public IActionResult Post_Detail_joinNow(int? id)
        {
            ViewBag.UserID = HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID);
            int UserID = ViewBag.UserID;
            if (id != null)
            {
                int p = db.TPosts.FirstOrDefault(p => p.FPostId == id).FPeople; //活動人數
                IEnumerable<TJoinPerson> query = db.TJoinPeople.Where(p => p.FPostId == id && p.FUserId == UserID); //有沒有報名
                if (query.Any() ==true)  //已參加
                {
                    db.TJoinPeople.Remove(query.FirstOrDefault());
                    db.SaveChanges();
                    //判斷目前報名人數 候補
                    if (p > db.TJoinPeople.Where(j => j.FPostId == id && j.FJoincheck == true).Count())
                    {
                        TJoinPerson j = db.TJoinPeople.FirstOrDefault(j => j.FPostId == id && j.FJoincheck == false);
                        if (j != null)
                        {
                            j.FJoincheck = true;
                            db.SaveChanges();
                        }
                        
                    }
                }
                else
                {
                    TJoinPerson joinPerson = null;
                    //判斷目前報名人數，T=不足(參加)，F=額滿(等待)。
                    if (p > db.TJoinPeople.Where(j => j.FPostId == id && j.FJoincheck == true).Count())
                    {
                        joinPerson = new() { FPostId = id, FUserId = UserID, FJoinTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm"), FJoincheck = true };
                    }
                    else
                    {
                        joinPerson = new() { FPostId = id, FUserId = UserID, FJoinTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm"), FJoincheck = false };
                    }
                    db.TJoinPeople.Add(joinPerson);
                    db.SaveChanges();

                }
                var people = db.TPosts.AsEnumerable().GroupJoin(db.TJoinPeople, p => p.FPostId, j => j.FPostId, (p, j) => (p, j)).FirstOrDefault(p => p.p.FPostId == id);
                var result = people.j.Join(db.Members, j => j.FUserId, m => m.FUserId, (j, m) => new Post_Join_ViewModel
                {
                    joinPerson = j,
                    member = m,
                }).OrderBy(p => p.joinPerson.FJoinId).Skip(1);
                return PartialView("Post_Detail_join", result);
            }
            return View("Post_Detail_post");
        }

        [HttpPost]
        public JsonResult joinBtncheck(int?id,int userid)
        {
            string x = "";
            ViewBag.UserID = HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID);
            if (ViewBag.UserID != null)
            {
                int UserID = ViewBag.UserID;
                var query = db.TPosts.AsEnumerable()
                                     .GroupJoin(db.TJoinPeople, p => p.FPostId, j => j.FPostId, (p, j) => (p, j))
                                     .FirstOrDefault(i => i.p.FPostId == id);
                if (query.p != null) 
                {
                    if (query.p.FUserId == UserID) x = "status0"; //主辦
                    else if (query.j.Where(p => p.FUserId == UserID && p.FJoincheck == true).Any() == true) x = "status1"; //參加
                    else if (query.j.Where(p => p.FUserId == UserID && p.FJoincheck == false).Any() == true) x = "status2";//候補
                    else if (query.j.Where(p => p.FUserId == UserID).Any() == false) x = "status3";//沒參加
                }
                
            }
            return Json(x);
        }

        

        //todo追蹤待處理
        //[HttpPost]
        //public IActionResult Post_Detail_track(int? id)
        //{
        //    return View("Post_Detail_post");
        //}



        //todo顯示評分
        [HttpPost]
        public IActionResult Post_Detail_star(int? id)
        {
            
            var query = db.TPostMsgs.Where(m => m.FPostId == id).Select(m => m.FStart).Average();
            if (query==null)
            {
                string staravg = "0%";
                return Json(new { query = "0", staravg });
            }
            else
            {

               string staravg = (Math.Round((double)query, 2) * 20).ToString() + "%";
                return Json(new { query= Math.Round((double)query, 1), staravg });
            }
            
        }

        [HttpPost]
        public IActionResult Post_Detail_likecount(int? id)

        {
            var query = lc(id);

            return PartialView("Post_Detail_likecount",query);
        }
        [HttpPost]
        public IActionResult Post_Detail_likeclick(int? id)
        {
            ViewBag.UserID = HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID);
            int UserID = ViewBag.UserID;
            var query = lc(id);
            if (query.ifliked != true)
            {
                TPostLikeCount postLikeCount = new TPostLikeCount
                {
                    FPostId = id,
                    FUserId = UserID
                };
                db.TPostLikeCounts.Add(postLikeCount);
                db.SaveChanges();
            }
            else
            {
                var uesrlike = db.TPostLikeCounts.FirstOrDefault(p => p.FPostId == id && p.FUserId == UserID);
                db.TPostLikeCounts.Remove(uesrlike);
                db.SaveChanges();
            }
            query = lc(id);
            return PartialView("Post_Detail_likecount", query);
        }



        [NonAction]
        public LikeCount lc(int? id) 
        {
            ViewBag.UserID = HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID);
            int UserID = ViewBag.UserID;
            string query = db.TPostLikeCounts.Where(p => p.FPostId == id).Count().ToString();
            bool qq = (db.TPostLikeCounts.Where(p => p.FPostId == id && p.FUserId == UserID).Any() == true);
            LikeCount likeCount = new()
            {
                Likecount = query,
                ifliked = qq
            };
            return likeCount;
        }

    }
}
