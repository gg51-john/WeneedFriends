using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using new_layout_core.Models;
using new_layout_core.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_layout_core.Controllers
{
    public class CPostDetailController : Controller
    {
        private readonly WeNeedFriendsFINContext db;

        public CPostDetailController(WeNeedFriendsFINContext wnf)
        {
            db = wnf;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post_Detail_message(int? id)
        {
            if (id != null)
            {
                var query = db.TPostMsgs.Where(p => p.FPostId == id)
                                        .Join(db.Members, p => p.FUserId, u => u.FUserId, (p, u) => new Post_Message
                                        {
                                            FPostId = p.FPostId,
                                            FPostMsgId = p.FPostMsgId,
                                            FUserPhoto = u.FUserPhoto,
                                            FMsgTime = p.FMsgTime/*DateTime.Now.ToString("yyyy/MM/dd HH:mm")*/,
                                            FUserName = u.FUserName,
                                            FMsgDesc = p.FMsgDesc,
                                            FStart = p.FStart,
                                        }).OrderByDescending(p => p.FPostMsgId);
                return PartialView("Post_Detail_message", query);
            }
            return View("Post_Detail_message", null);
        }
        [HttpPost]
        public IActionResult Post_Detail_replay(int? msgid)
        {
            if (msgid != null)
            {
                var query = db.TPostMsgeds.Where(p => p.FPostMsgId == msgid)
                                        .Join(db.Members, p => p.FUserId, u => u.FUserId, (p, u) => new Post_Message_Reply
                                        {
                                            FPostMsgedId = p.FPostMsgedId,
                                            FPostMsgId = (int)p.FPostMsgId,
                                            FUserPhoto = u.FUserPhoto,
                                            FMsgedTime = p.FMsgedTime,
                                            FUserName = u.FUserName,
                                            FMsgedDesc = p.FMsgedDesc,

                                        });
                return PartialView("Post_Detail_replay", query);
            }
            return PartialView("Post_Detail_replay", null);
        }

        [HttpPost]
        public IActionResult message_new(TPostMsg postMsg)
        {
            ViewBag.UserID = HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID);
            int UserID = ViewBag.UserID;
            IEnumerable<Post_Message> query = null;
            try
            {
                postMsg.FMsgTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                postMsg.FUserId = UserID;
                db.TPostMsgs.Add(postMsg);
                db.SaveChanges();
                query = db.TPostMsgs.Where(p => p.FPostId == postMsg.FPostId)
                                        .Join(db.Members, p => p.FUserId, u => u.FUserId, (p, u) => new Post_Message
                                        {
                                            FPostId = p.FPostId,
                                            FPostMsgId = p.FPostMsgId,
                                            FUserPhoto = u.FUserPhoto,
                                            FMsgTime = p.FMsgTime/*DateTime.Now.ToString("yyyy/MM/dd HH:mm")*/,
                                            FUserName = u.FUserName,
                                            FMsgDesc = p.FMsgDesc,
                                            FStart = p.FStart,
                                        }).OrderByDescending(p => p.FPostMsgId);
                if (query != null) {
                    string stars = Math.Round( query.Average(p => p.FStart).Value,1).ToString();
                    var tpost = db.TPosts.FirstOrDefault(p => p.FPostId == postMsg.FPostId);
                    tpost.FLikeCount = stars;
                    db.SaveChanges();

                }
                
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
            return PartialView("Post_Detail_message", query);
        }

        [HttpPost]
        public IActionResult reply_new(TPostMsged postMsged)
        {
            ViewBag.UserID = HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID);
            int UserID = ViewBag.UserID;
            postMsged.FMsgedTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            postMsged.FUserId = UserID;
            db.TPostMsgeds.Add(postMsged);
            db.SaveChanges();
            var query = db.TPostMsgeds.Where(p => p.FPostMsgId == postMsged.FPostMsgId)
                                        .Join(db.Members, p => p.FUserId, u => u.FUserId, (p, u) => new Post_Message_Reply
                                        {
                                            FPostMsgedId = p.FPostMsgedId,
                                            FPostMsgId = (int)p.FPostMsgId,
                                            FUserPhoto = u.FUserPhoto,
                                            FMsgedTime = p.FMsgedTime,
                                            FUserName = u.FUserName,
                                            FMsgedDesc = p.FMsgedDesc,

                                        });
            //}
            return PartialView("Post_Detail_replay", query);
        }



        //檢舉System
        [HttpPost]
        public IActionResult Post_Msg_Report(TPostMsgReport postMsgReport)
        {
            string result=null;
            try 
            {
                if (postMsgReport != null && !db.TPostMsgReports.Any(p=>p.FPostMsgId==postMsgReport.FPostMsgId &&p.FUserId==postMsgReport.FUserId))
                {
                    
                    db.TPostMsgReports.Add(postMsgReport);
                    db.SaveChanges();
                    result = "留言檢舉成功";
                }
                else result = "您已檢舉過該留言";
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }
        [HttpPost]
        public IActionResult Post_Msged_Report(TPostMsgedReport postMsgedReport)
        {
            string result = null;
            try
            {
                if (postMsgedReport != null && !db.TPostMsgedReports.Any(p => p.FPostMsgedId == postMsgedReport.FPostMsgedId && p.FUserId == postMsgedReport.FUserId))
                {

                    db.TPostMsgedReports.Add(postMsgedReport);
                    db.SaveChanges();
                    result = "留言檢舉成功";
                }
                else result = "您已檢舉過該留言";
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }
        [HttpPost]
        public IActionResult Post_Post_Report(TPostReport postReport)
        {
            string result = null;
            try
            {
                if (postReport != null && !db.TPostReports.Any(p => p.FPostId == postReport.FPostId && p.FUserId == postReport.FUserId))
                {

                    db.TPostReports.Add(postReport);
                    db.SaveChanges();
                    result = "留言檢舉成功";
                }
                else result = "您已檢舉過該活動";
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
        }
        [HttpPost]
        public IActionResult Post_Post_Store(TPostStore postStore)
        {
            string result = null;
            try
            {
                var query = db.TPostStores;
                if (postStore != null && !query.Any(p => p.FPostId == postStore.FPostId && p.FUserId == postStore.FUserId))
                {

                    db.TPostStores.Add(postStore);
                    db.SaveChanges();
                    result = "收藏活動成功";
                }
                else
                {
                    var q = query.Where(p => p.FPostId == postStore.FPostId && p.FUserId == postStore.FUserId);
                    db.TPostStores.RemoveRange(q);
                    db.SaveChanges();
                    result = "取消收藏";
                }
                    
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return Json(result);
            
        }
    }
}
