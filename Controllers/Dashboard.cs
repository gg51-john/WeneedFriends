using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Utils;
using new_layout_core.Models;
using new_layout_core.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace new_layout_core.Controllers
{
    public class Dashboard : Controller
    {        
        WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
                
        private IHostingEnvironment iv_host;
        public Dashboard(IHostingEnvironment p)
        {
            iv_host = p;
        }
        //儀錶板
        public IActionResult Dashboardview()
        {
            //ViewBag.USERID = HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID);
            return PartialView();
        }
        //電子報編輯頁面
        public IActionResult newsletter()
        {
            return PartialView();
        }
        //留言/檢舉管理
        public IActionResult showallreport()
        {
            return PartialView();
        }
        public IActionResult Home()
        {
            return View();
        }
        //載入會員資料
        public JsonResult loadmember()
        {
            var q = db.Members.Select(n => n);
            return Json(q);
        }
        //檢查email
        public IActionResult checkemail(string email)
        {
            var q = db.Members.FirstOrDefault(n => n.FEmail.Equals(email));
            if (q != null)
            {
                return Json("false");
            }
            return Json("true");
        }
        //會員新增
        [HttpPost]
        public JsonResult memberAdd(MemberViewModel m)
        {
            if (m != null)
            {
                if (m.image == null)
                {
                    return Json("false"); 
                }
                if (m.FEmail == null)
                {
                    return Json("false");
                }
                if (m.FAddress == null)
                {
                    return Json("false");
                }
                if (m.FCity == null)
                {
                    return Json("false");
                }
                if (m.FDistrict == null)
                {
                    return Json("false");
                }
                if (m.FGender == null)
                {
                    return Json("false");
                }
                if (m.FPassword == null)
                {
                    return Json("false");
                }
                if (m.FUserBirthday == null)
                {
                    return Json("false");
                }
                if (m.FUserPhone == null)
                {
                    return Json("false");
                }
                if (m.FUserName == null)
                {
                    return Json("false");
                }
                var k = db.Members.FirstOrDefault(n => n.FEmail.Equals(m.FEmail));
                if (k != null)
                {
                    return Json("emailrepeat");
                }
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                using (
                    var photo = new FileStream(
                    iv_host.ContentRootPath + @"\wwwroot\images\" + photoName,
                    FileMode.Create))
                {
                    m.image.CopyTo(photo);
                }
                m.FUserPhoto = "/images/" + photoName;
                db.Members.Add(m.member);
                db.SaveChanges();
                return Json("success");
            }

            //var q = db.Members.FirstOrDefault(n => n.FEmail.Equals(m.FEmail));
            //    if (q == null)
            //    {                
            //    string photoName = Guid.NewGuid().ToString() + ".jpg";
            //    using (
            //        var photo = new FileStream(
            //        iv_host.ContentRootPath + @"\wwwroot\images\" + photoName,
            //        FileMode.Create))
            //    {
            //        m.image.CopyTo(photo);
            //    }
            //    m.FUserPhoto = "../images/" + photoName;
            //    db.Members.Add(m.member);
            //    db.SaveChanges();
            //    return Json("success");
            //}             
            return Json(false);
            
        }
        //會員單筆刪除
        public JsonResult memberdelete(int? id)
        {
            if (id != null)
            {
                Member q = db.Members.FirstOrDefault(n => n.FUserId == id);
                if (q != null)
                {
                    db.Remove(q);
                    db.SaveChanges();
                }
            }
            return Json(id);
        }
        //拿會員id
        public JsonResult getmemberid(int? id)
        {
            var q = db.Members.Where(n => n.FUserId.Equals(id));

            return Json(q);

        }
        //會員多筆刪除
        [HttpPost]
        public IActionResult memberdeleteall(int[] id)
        {
            if (id != null)
            {





                var TPostMsgeds = db.TPostMsgeds.Where(n => id.Contains((int)n.FUserId));
                db.TPostMsgeds.RemoveRange(TPostMsgeds);
                
                var TPostMsgs = db.TPostMsgs.Where(n => id.Contains((int)n.FUserId));
                db.TPostMsgs.RemoveRange(TPostMsgs);
                         
                


                var likecount = db.TPostLikeCounts.Where(n => id.Contains((int)n.FUserId));
                db.TPostLikeCounts.RemoveRange(likecount);

                var tpost2 = db.TPosts.Where(n => id.Contains((int)n.FUserId));
                db.TPosts.RemoveRange(tpost2);

                var tpostd = db.TPosts.Where(n => id.Contains((int)n.FUserId)).Select(n=>n.FPostId).ToArray();
                var photo = db.TPostPhotos.Where(n => tpostd.Contains((int)n.FPostId));
                db.TPostPhotos.RemoveRange(photo);



                var forder = db.TmyBuyDetails.Where(n => id.Contains((int)n.FUserId));
                db.TmyBuyDetails.RemoveRange(forder);

                var membertag = db.TMemberTags.Where(n => id.Contains((int)n.UserId));
                db.TMemberTags.RemoveRange(membertag);

                
                






                var joinpeople = db.TJoinPeople.Where(n => id.Contains((int)n.FUserId));
                db.TJoinPeople.RemoveRange(joinpeople);

                var artstore = db.TArticleStores.Where(n => id.Contains((int)n.UserId));
                db.TArticleStores.RemoveRange(artstore);

                var artreson = db.TArticleReportSons.Where(n => id.Contains((int)n.UserId));
                db.TArticleReportSons.RemoveRange(artreson);

                var artrecheck = db.TArticleReportChecks.Where(n => id.Contains((int)n.UserId));
                db.TArticleReportChecks.RemoveRange(artrecheck);

                var artreport = db.TArticleReports.Where(n => id.Contains((int)n.UserId));
                db.TArticleReports.RemoveRange(artreport);

                var likee = db.TArticleLikes.Where(n => id.Contains((int)n.UserId));
                db.TArticleLikes.RemoveRange(likee);

                var check = db.TArticleChecks.Where(n => id.Contains((int)n.UserId));
                db.TArticleChecks.RemoveRange(check);

                var article = db.TArticles.Where(n => id.Contains((int)n.UserId));
                db.TArticles.RemoveRange(article);

                //var posreport = db.PostReports.Where(n => id.Contains((int)n.UserId));
                //db.PostReports.RemoveRange(posreport);

                var q = db.Members.Where(n => id.Contains(n.FUserId));
                db.Members.RemoveRange(q);
                db.SaveChanges();
            }
            return Json(id);
        }
        //會員修改
        [HttpPost]
        public JsonResult memberupdate(Member member)
        {
            if (member != null)
            {
                Member q = db.Members.FirstOrDefault(n => n.FUserId.Equals(member.FUserId));
                if (q != null)
                {
                    q.FUserName = member.FUserName;
                    q.FEmail = member.FEmail;
                    q.FUserPhone = member.FUserPhone;                    

                    db.SaveChanges();
                }
            }
            return Json(member);
        }
        [HttpPost]
        //修改會員權限成一般訪客
        public JsonResult changestatus(int? id)
        {
            if (id != null)
            {
                Member q = db.Members.FirstOrDefault(n => n.FUserId==id);
                if (q != null)
                {
                    if (q.Fstatus == 1)
                        q.Fstatus = 2;
                    else if (q.Fstatus == 2)
                        q.Fstatus = 1;                   
                    db.SaveChanges();
                }
            }
            return Json(id);

        }
        //寄單筆mail(html to body)
        public IActionResult hhh(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("handsomejack", "hasnonametouse@gmail.com"));
            string[] Multiple = to.Split(',');
            foreach (string multiple_email in Multiple)
            {
                message.To.Add(new MailboxAddress(multiple_email));
            }
            message.Subject = subject;

            var builder = new BodyBuilder();
            //var image = builder.LinkedResources.Add(@"E:\目前的測試\sendemail\testemail\testemail\wwwroot\imgs\02-哥吉拉大戰金剛-1-1616694970.jpeg");
            //image.ContentId = MimeUtils.GenerateMessageId();
            builder.TextBody = body;
           // builder.HtmlBody = string.Format(@"<p>123<br>

           //<center><img src=""cid:{0}""></center>", image.ContentId);


            //builder.HtmlBody = string.Format(body);
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587);
                client.Authenticate("hasnonametouse@gmail.com", "@Momopalace123@");
                client.Send(message);
                client.Disconnect(true);
            }

            return Json(to);
        }


        //所有文章
        public IActionResult articlelist()
        {
            return View();
        }
        //載入文章資料
        public JsonResult loadarticle()
        {
            var article = from n in db.TArticles
                          join k in db.Members on n.UserId equals k.FUserId
                          select new
                          {
                              title = n.ArtTitle,
                              username = k.FUserName,
                              date = n.Date,
                              articleid = n.ArticleId
                          };
                          

            return Json(article);
        }
        //文章新增
        [HttpPost]
        public JsonResult articleAdd(TArticle article)
        {
            db.Add(article);
            db.SaveChanges();
            return Json(article);
        }
        //文章多筆刪除
        [HttpPost]
        public IActionResult articledeleteall(int[] id)
        {
            if (id != null)
            {
                var tag = db.TArticleTags.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleTags.RemoveRange(tag);

                var q = db.TArticles.Where(n => id.Contains(n.ArticleId)).Select(n => n.ArticleId).ToList();
                var k = db.TArticleReports.Where(n => q.Contains((int)n.ArticleId)).Select(n => n.ArticleReportId).ToList();

                var s = db.TArticleReportSons.Where(n => k.Contains(n.ArticleReportId));

                db.TArticleReportSons.RemoveRange(s);
                var h = db.TArticleReports.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleReports.RemoveRange(h);

                var like = db.TArticleLikes.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleLikes.RemoveRange(like);

                var store = db.TArticleStores.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleStores.RemoveRange(store);

                var x = db.TArticles.Where(n => id.Contains(n.ArticleId));
                db.TArticles.RemoveRange(x);
                var y = db.TArticleChecks.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleChecks.RemoveRange(y);

                db.SaveChanges();
            }
            return Json(id);
        }
        //所有活動
        public JsonResult actlist()
        {
            var q = from n in db.TPosts
                    join k in db.Members on n.FUserId equals k.FUserId
                    select new
                    {
                        fTitle = n.FTitle,
                        fDescription = n.FDescription,
                        name = k.FUserName,
                        fPostId = n.FPostId,
                        fPostTime = n.FPostTime
                    };

            return Json(q);
        }
        //活動新增
        [HttpPost]
        public JsonResult postAdd(TPost post)
        {
            db.Add(post);
            db.SaveChanges();
            return Json(post);
        }
        //活動多筆刪除
        [HttpPost]
        public IActionResult postdeleteall(int[] id)
        {
            if (id != null)
            {
                var q = db.TPosts.Where(n => id.Contains(n.FPostId));
                db.TPosts.RemoveRange(q);
                db.SaveChanges();
            }
            return Json(id);
        }
        //找所有人的mail
        public IActionResult findemail()
        {
            var q = db.Members.Select(n => n.FEmail);
            return Json(q);
        }
        //電子報用
        public IActionResult DM(string to, string subject, string body)
        {
            string[] q = db.Members.Select(n => n.FEmail).ToArray();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("魔人揪揪團隊", "hasnonametouse@gmail.com"));
            //string[] Multiple = q.Split(',');
            foreach (string multiple_email in q)
            {
                message.To.Add(new MailboxAddress(multiple_email));
            }
            message.Subject = subject;

            var builder = new BodyBuilder();
            //var image = builder.LinkedResources.Add(@"C:\Users\User\Desktop\sln_Test\Test\wwwroot\images\Jins-newsletter.png");
            //image.ContentId = MimeUtils.GenerateMessageId();
            //var image1 = builder.LinkedResources.Add(@"D:\目前的測試\new_layout_core\new_layout_core\wwwroot\images\de6ae1f3-9d0e-46c1-aca1-34a459356d33.jpg");
            //image1.ContentId = MimeUtils.GenerateMessageId();
            //builder.HtmlBody = body;
            builder.HtmlBody = body;
                
                //string.Format(@"<h2>測試用電子報<br><center><img src=""cid:{0}""></center></center>", image.ContentId

            //);


            //builder.HtmlBody = string.Format(body);
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587);
                client.Authenticate("hasnonametouse@gmail.com", "@Momopalace123@");
                client.Send(message);
                client.Disconnect(true);
            }

            return Json(to);
        }
        //顯示所有被檢舉的文章
        public IActionResult showarticlecheck()
        {

            var q = from n in db.TArticleChecks
                    group n by new { n.ArticleId, n.Article.ArtTitle,n.UserId } into g
                    select new
                    {
                        title = g.Key.ArtTitle,
                        articleid = g.Key.ArticleId,
                        count = g.Count(),
                        userid = g.Key.UserId
                    };
            return Json(q);
        }
        //顯示被檢舉文章的細項
        public IActionResult showlist(int? id)
        {
            var q = db.TArticleChecks.Where(n => n.ArticleId == id);
            return Json(q);
        }
        public IActionResult showarticlereport()
        {
            var q = from n in db.TArticleReports
                    join k in db.TArticles on n.ArticleId equals k.ArticleId
                    from g in db.Members
                    where n.UserId==g.FUserId
                    
                    select new
                    {
                        title = k.ArtTitle,
                        username = g.FUserName,
                        reportid = n.ArticleReportId,
                        reporttime = n.ArticleReportTime,
                        message = n.ArticleReport
                    };
            return Json(q);
        }
        
        //帳號停權
        [HttpPost]
        public IActionResult testrecoverytime(int[] id, ChangeStatusDate p)
        {
            if (id != null)
            {
                Member q = db.Members.FirstOrDefault(n => id.Contains(n.FUserId));
                if (q != null)
                {
                    q.Fstatus = 2;

                    db.Add(p);
                    db.SaveChanges();
                }

            }
            return Json(p);
        }
        //會員總數
        public IActionResult membernumber()
        {
            var q = db.Members.Count();
            return Json(q);
        }
        //文章總數
        public IActionResult articlecount()
        {
            var q = db.TArticles.Count();
            return Json(q);
        }
        //活動總數
        public IActionResult postcount()
        {
            var q = db.TPosts.Count();
            return Json(q);
        }
        //刪留言
        [HttpPost]
        public IActionResult deletarticlereport(int[] id)
        {
            if (id != null)
            {
                var s = db.TArticleReportSons.Where(n => id.Contains(n.ArticleReportId));
                db.TArticleReportSons.RemoveRange(s);                
                

                var q = db.TArticleReports.Where(n => id.Contains(n.ArticleReportId));
                db.TArticleReports.RemoveRange(q);                
                db.SaveChanges();
            }
            return Json(id);
        }
        //刪文章
        [HttpPost]
        public IActionResult deletarticle(int[] id,Memberjail p)
        {
            if (id != null)
            {       
                
                


                var tag = db.TArticleTags.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleTags.RemoveRange(tag);

                var q = db.TArticles.Where(n => id.Contains(n.ArticleId)).Select(n => n.ArticleId).ToList();
                var k = db.TArticleReports.Where(n => q.Contains((int)n.ArticleId)).Select(n => n.ArticleReportId).ToList();
                
                var s = db.TArticleReportSons.Where(n => k.Contains(n.ArticleReportId));
                
                db.TArticleReportSons.RemoveRange(s);
                var h = db.TArticleReports.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleReports.RemoveRange(h);

                var like = db.TArticleLikes.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleLikes.RemoveRange(like);

                var store = db.TArticleStores.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleStores.RemoveRange(store);

                var x = db.TArticles.Where(n => id.Contains(n.ArticleId));
                db.TArticles.RemoveRange(x);
                var y = db.TArticleChecks.Where(n => id.Contains((int)n.ArticleId));
                db.TArticleChecks.RemoveRange(y);

                db.SaveChanges();
            }
            return Json(id);
        }
        //發送密碼
        [HttpPost]
        public IActionResult sendpasswordemail(int? id)
        {
            Member member = db.Members.FirstOrDefault(n => n.FUserId.Equals(id));
            if (member != null)
            {
                string useremail = member.FEmail;
                string username = member.FUserName;
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("魔人揪揪團隊", "hasnonametouse@gmail.com"));
                message.To.Add(new MailboxAddress(username, useremail));
                message.Subject = "魔人揪揪團隊發送";
                var builder = new BodyBuilder();
                builder.TextBody = "您的新密碼為:Nihaoban666";
                message.Body = builder.ToMessageBody();
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 587);
                    client.Authenticate("hasnonametouse@gmail.com", "@Momopalace123@");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            return Json(id);
        }
        //顯示集中營
        //public IActionResult showalljail()
        //{
        //    var q = from n in db.Memberjails
        //            from k in db.Members
        //            where n.Userid == k.FUserId
        //            select new
        //            {
        //                name = k.FUserName,
        //                reason = n.Reason,
        //                date = n.Date,
        //                status = k.Fstatus,
        //                userid = n.Userid
        //            };
        //    return Json(q);
        //}
        //登出
        public IActionResult logoutandclear(int? id)
        {
            if (id != null || HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID) != null)
            {
                HttpContext.Session.Clear();              
            }
            return Json(id);
        }
        //這是測試用電子報
        public IActionResult practicenewsletter(string to, string subject, string body)
        {
            string[] q = db.Members.Select(n => n.FEmail).ToArray();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("魔人揪揪團隊", "hasnonametouse@gmail.com"));
            //string[] Multiple = q.Split(',');
            foreach (string multiple_email in q)
            {
                message.To.Add(new MailboxAddress(to));
            }
            message.Subject = subject;

            var builder = new BodyBuilder();
            //var image = builder.LinkedResources.Add(@"C:\Users\User\Desktop\sln_Test\Test\wwwroot\images\Jins-newsletter.png");
            //image.ContentId = MimeUtils.GenerateMessageId();
            //var image1 = builder.LinkedResources.Add(@"D:\目前的測試\new_layout_core\new_layout_core\wwwroot\images\de6ae1f3-9d0e-46c1-aca1-34a459356d33.jpg");
            //image1.ContentId = MimeUtils.GenerateMessageId();
            //builder.HtmlBody = body;
            builder.HtmlBody = body;

            //string.Format(@"<h2>測試用電子報<br><center><img src=""cid:{0}""></center></center>", image.ContentId

            //);


            //builder.HtmlBody = string.Format(body);
            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587);
                client.Authenticate("hasnonametouse@gmail.com", "@Momopalace123@");
                client.Send(message);
                client.Disconnect(true);
            }

            return Json(to);
        }



    }
}
