using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using new_layout_core.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using MimeKit;
using MailKit.Net.Smtp;

namespace new_layout_core.Controllers
{
    public class test : Controller
    {
        public IActionResult hhhh()
        {
            return PartialView();
        }
        public IActionResult Login()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(CDictionary.LOGIN_AUTHTICATION_CODE)))
            {
                Random r = new Random();
                string code = r.Next(0, 10).ToString() +
                    r.Next(0, 10).ToString() +
                    r.Next(0, 10).ToString() +
                    r.Next(0, 10).ToString();
                HttpContext.Session.SetString(CDictionary.LOGIN_AUTHTICATION_CODE, code);
                //ViewBag.PASSWORD = HttpContext.Session.GetInt32(CDictionary.LOGIN_AUTHTICATION_CODE);
            }
            ViewData[CDictionary.LOGIN_AUTHTICATION_CODE] =
                HttpContext.Session.GetString(CDictionary.LOGIN_AUTHTICATION_CODE);
            return PartialView();
        }
        public IActionResult Create()
        {            
            return PartialView();
        }
        public IActionResult memberview()
        {
            ViewBag.MEMBER = HttpContext.Session.GetString(CDictionary.CURRENT_LOGINED_USERNAME);            
            return View();
        }
        [HttpPost]
        public IActionResult Login(string p, string date,string password,string random)
        {
            WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
            Member member = db.Members.FirstOrDefault(n => n.FEmail.Equals(p)&&n.FPassword.Equals(password));
            if (random == null)
            {
                return Json("0");
            }
            if (member != null&& random.Equals(HttpContext.Session.GetString(CDictionary.LOGIN_AUTHTICATION_CODE)))
            {
                var q = db.ChangeStatusDates.Where(n => n.Userid.Equals(member.FUserId) && n.Status == true).ToList();
                if (q != null)
                {
                    foreach (var item in q)
                    {
                        long time = long.Parse(item.Recoverytime);
                        long today = long.Parse(date);
                        if (time - today <= 0)
                        {
                            item.Status = false;
                            member.Fstatus = 1;
                            db.SaveChanges();
                            HttpContext.Session.SetInt32(CDictionary.CURRENT_LOGINED_USERSTATUS, (int)member.Fstatus);
                            HttpContext.Session.SetString(CDictionary.CURRENT_LOGINED_USERNAME, member.FUserName);
                            HttpContext.Session.SetInt32(CDictionary.CURRENT_LOGINED_USERID, member.FUserId);
                            HttpContext.Session.SetString(CDictionary.CURRENT_LOGINED_USERPHOTO, member.FUserPhoto);
                            return Json(member.Fstatus);
                        }                        

                        return Json(member);
                    }
                }               

                if ( member.Fstatus == 1)
                {
                    HttpContext.Session.SetInt32(CDictionary.CURRENT_LOGINED_USERSTATUS, (int)member.Fstatus);
                    HttpContext.Session.SetString(CDictionary.CURRENT_LOGINED_USERNAME, member.FUserName);
                    HttpContext.Session.SetInt32(CDictionary.CURRENT_LOGINED_USERID, member.FUserId);
                    HttpContext.Session.SetString(CDictionary.CURRENT_LOGINED_USERPHOTO, member.FUserPhoto);

                    return Json(member);
                }
                

                if (member.Fstatus == 3)
                {
                    HttpContext.Session.SetInt32(CDictionary.CURRENT_LOGINED_USERSTATUS, (int)member.Fstatus);
                    HttpContext.Session.SetString(CDictionary.CURRENT_LOGINED_USERNAME, member.FUserName);
                    HttpContext.Session.SetInt32(CDictionary.CURRENT_LOGINED_USERID, member.FUserId);
                    HttpContext.Session.SetString(CDictionary.CURRENT_LOGINED_USERPHOTO, member.FUserPhoto);

                    return Json(member);
                }
                
            }
            else if(member == null || !random.Equals(HttpContext.Session.GetString(CDictionary.LOGIN_AUTHTICATION_CODE)))
            {
                return Json("0");
            }
            return Json(member);
        }
        //忘記密碼
        public IActionResult ForgetPassWord()
        {

            return PartialView();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult ForgetPassWord(string subject, string body, string to)
        {
            WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
            if (to != null)
            {
                Member email = db.Members.FirstOrDefault(n => n.FEmail.Equals(to));
                if (email ==null)
                {
                    return Json("false");
                }
                if (email != null)
                {                    
                    //HttpContext.Session.SetInt32(CDictionary.CURRENT_FORGOTPASSWORD_USERID,email.FUserId);
                    //ViewBag.USERFG = HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID);
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("魔人揪揪團隊", "hasnonametouse@gmail.com"));

                    message.To.Add(new MailboxAddress(to));

                    message.Subject = subject;

                    var builder = new BodyBuilder();


                    //builder.HtmlBody = body;
                    builder.HtmlBody =
                        string.Format(@"<p>您的暫時密碼如下:</p></br>
                                        <h2>12345</h2>");
                    message.Body = builder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Connect("smtp.gmail.com", 587);
                        client.Authenticate("hasnonametouse@gmail.com", "@Momopalace123@");
                        client.Send(message);
                        client.Disconnect(true);
                    }

                    return Json(email.FUserId);
                }

            }           
            
            return Json("false");
        }
        //確認驗證碼
        public IActionResult identifyrandom(int? password)
        {
            //ViewBag.USERFG = HttpContext.Session.GetInt32(CDictionary.CURRENT_LOGINED_USERID);
            if (password == 12345)
            {
                return Json("true");
            }
            return Json("false");
        }

        //重設密碼雙重驗證
        public IActionResult redirectpassword(int? first,int? second,int? id)
        {
            WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
            if (first == second)
            {
                Member member = db.Members.FirstOrDefault(n => n.FUserId.Equals(id));
                member.FPassword = first.ToString();
                db.SaveChanges();
                return Json("true");
            }


            return Json("false"); 
        }
    }    
}
