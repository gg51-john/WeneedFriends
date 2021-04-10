using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using new_layout_core.Models;
using new_layout_core.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace new_layout_core.Controllers
{
    public class CPostCRUDController : Controller
    {
        static int user;
        WeNeedFriendsFINContext context = new WeNeedFriendsFINContext();
        //public IActionResult Post_All()
        //{
        //    try
        //    {
        //        var allpost = context.TPosts.ToList().GroupJoin(
        //            context.TPostPhotos, post => post.FPostId, photo => photo.FPostId,
        //            (post, photo) => new CPostViewModel
        //            {
        //                FPostId = post.FPostId,
        //                FTitle = post.FTitle,
        //                FPeople = post.FPeople,
        //                FPostAddress = post.FPostAddress,
        //                FPostCity = post.FPostCity,
        //                FPostCheck = post.FPostCheck,
        //                FPostDistrict = post.FPostDistrict,
        //                FPostTime = post.FPostTime,
        //                FDescription = post.FDescription,
        //                FLikeCount = post.FLikeCount,
        //                FSportName = post.FSportName,
        //                FSystemTime = post.FSystemTime,
        //                FUserId = post.FUserId,
        //                photo = photo,
        //                joinpeople = context.TJoinPeople.Where(n => n.FPostId == post.FPostId).Count()
        //            });
        //        return View(allpost);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    public IActionResult Post_Detail()
        {
            return View();
        }
        public IActionResult Post_Create()
        {
            return View();
        }
        private IHostingEnvironment iv_host;
        public CPostCRUDController(IHostingEnvironment p)
        {
            iv_host = p;
        }
        static List<string> Tag = new List<string>();
        [HttpPost]
        public IActionResult Post_Create(CPostViewModel cPost,string[] tag)
        {
            try
            {
                for (int i = 0; i < tag.Count(); i++)
                {
                    Tag.Add(tag[i]);
                }
                user = cPost.FUserId;
                //context.Add(cPost);

                TPost new_Post = new TPost
                {
                    FUserId = cPost.FUserId, //todo
                    FDescription = cPost.FDescription,
                    FPeople = cPost.FPeople,
                    FPostCity = cPost.FPostCity,
                    FPostAddress = cPost.FPostAddress,
                    FPostDistrict = cPost.FPostDistrict,
                    FPostTime = cPost.FPostTime,
                    FSportName = cPost.FSportName,
                    FSystemTime = DateTime.Now.ToString(),
                    FTitle = cPost.FTitle,
                };
                context.TPosts.Add(new_Post);
                context.SaveChanges();

                foreach (var i in Tag) //標籤
                {
                    TPostTag postTag = new TPostTag();
                    postTag.FTagId = int.Parse(i);
                    postTag.FPostId = new_Post.FPostId;
                    context.TPostTags.Add(postTag);
                    context.SaveChanges();
                }

                TJoinPerson joinPerson = new TJoinPerson(); //參加人
                joinPerson.FUserId = new_Post.FUserId;
                joinPerson.FPostId = new_Post.FPostId;
                joinPerson.FJoinTime = DateTime.Now.ToString();

                TPostSport postSport = new TPostSport();  //活動種類
                postSport.FPostId = new_Post.FPostId;
                postSport.FSportName = new_Post.FSportName;


                context.TJoinPeople.Add(joinPerson);
                context.SaveChanges();
                if (cPost.image != null)
                {
                    foreach (var i in cPost.image)
                    {
                        string photoName = Guid.NewGuid().ToString() + ".jpg";
                        using (var photo = new FileStream(iv_host.ContentRootPath + @"/wwwroot/Postimg/" + photoName, FileMode.Create))
                        {
                            i.CopyTo(photo);
                        }
                        TPostPhoto postPhoto = new TPostPhoto();
                        postPhoto.FPostId = new_Post.FPostId;
                        postPhoto.FPostPhoto = @"/Postimg/" + photoName;
                        context.TPostPhotos.Add(postPhoto);
                        context.SaveChanges();
                    }
                }
                return RedirectToAction("Post_Index", "CPost");
        }
            catch
            {
                return RedirectToAction("Post_Index", "CPost");
            }
        }
        public IActionResult Post_Edit(int id)
        {
            try
            {

                var allpost = context.TPosts.Where(m => m.FUserId == id).Select(n => new CPost_Edit()
                {
                    FPostId = n.FPostId,
                    FSportName = n.FSportName,
                    FTitle = n.FTitle,
                    FUserId = n.FUserId
                });
                return View(allpost);
            }
            catch
            {
                return RedirectToAction("Post_Index", "CPost");
            }
        }
        public IActionResult Edit(int? FPostId)
        {
            try
            {
                if (FPostId != null)
                {
                    var allpost = context.TPosts.ToList().GroupJoin(
                        context.TPostPhotos, post => post.FPostId, photo => photo.FPostId,
                        (post, photo) => new CPostViewModel
                        {
                            FPostId = post.FPostId,
                            FTitle = post.FTitle,
                            FPeople = post.FPeople,
                            FPostAddress = post.FPostAddress,
                            FPostCity = post.FPostCity,
                            FPostCheck = post.FPostCheck,
                            FPostDistrict = post.FPostDistrict,
                            FPostTime = post.FPostTime,
                            FDescription = post.FDescription,
                            FLikeCount = int.Parse(post.FLikeCount),
                            FSportName = post.FSportName,
                            FSystemTime = post.FSystemTime,
                            FUserId = post.FUserId,
                            photo = photo
                        }).FirstOrDefault(n => n.FPostId == FPostId);
                    return View(allpost);
                }
                return RedirectToAction("Post_Edit");
            }
            catch
            {
                return RedirectToAction("Post_Index", "CPost");
            }
        }
        public IActionResult Edit_img(string img)
        {
            try
            {
                TPostPhoto photo = context.TPostPhotos.FirstOrDefault(n => n.FPostPhotoId == int.Parse(img));
                context.TPostPhotos.Remove(photo);
                context.SaveChanges();
                return RedirectToAction("Edit");
            }
            catch
            {
                return RedirectToAction("Post_Index", "CPost");
            }
        }
        [HttpPost]
        public IActionResult Edit(CPostViewModel post)
        {
            try
            {
                if (post != null)
                {
                    TPost P_被修改 = context.TPosts.FirstOrDefault(n => n.FPostId == post.FPostId);

                    if (P_被修改 != null)
                    {
                        P_被修改.FTitle = post.FTitle;
                        P_被修改.FDescription = post.FDescription;
                        P_被修改.FPeople = post.FPeople;
                        P_被修改.FPostAddress = post.FPostAddress;
                        P_被修改.FPostCity = post.FPostCity;
                        P_被修改.FPostDistrict = post.FPostDistrict;
                        P_被修改.FPostTime = post.FPostTime;
                        P_被修改.FSportName = post.FSportName;
                        P_被修改.FSystemTime = DateTime.Now.ToString();
                        P_被修改.FPostCheck = post.FPostCheck;
                        P_被修改.FLikeCount = post.FLikeCount.ToString();
                        //TJoinPerson joinPerson = new TJoinPerson(); //參加人
                        //joinPerson.FUserId = post.FUserId;
                        //joinPerson.FPostId = post.FPostId;

                        
                        if (post.image != null)
                        {
                            foreach (var i in post.image)
                            {
                                string PhotoName = Guid.NewGuid().ToString() + ".jpg";
                                using (var photo = new FileStream(iv_host.ContentRootPath + @"/wwwroot/img/" + PhotoName, FileMode.Create))
                                {
                                    i.CopyTo(photo);
                                }
                                TPostPhoto tp = new TPostPhoto();
                                tp.FPostPhoto = "~/img/" + PhotoName;
                                tp.FPostId = post.FPostId;
                                context.TPostPhotos.Add(tp);
                                context.SaveChanges();
                            }
                        }
                        context.SaveChanges();
                    }
                }
                return RedirectToAction("Post_Index", "CPost");
            }
            catch
            {
                return RedirectToAction("Post_Index", "CPost");
            }
        }
        public JsonResult getDistrict(string city)
        {
            try
            {
                if (city != "請選擇")
                {
                    var cityID = new WeNeedFriendsFINContext().TCities.FirstOrDefault(n => n.FCityName == city);
                    var district = new WeNeedFriendsFINContext().TLocations.Where(d => d.FCityId == cityID.FCityId).Select(n => n.FDistrictName);
                    return Json(district);
                }
                return Json(0);
            }
            catch
            {
                return Json(0);
            }
        }
        public IActionResult Delete(int? FPostId)
        {
            try
            {
                if (FPostId != null)
                {
                    var postMsged = context.TPostMsgeds.Where(n => n.FPostId == FPostId);
                    if (postMsged != null)
                    {
                        foreach (var msged in postMsged)
                        {
                            context.TPostMsgeds.Remove(msged);
                        }
                    }

                    var postCategory = context.TPostTags.Where(n => n.FPostId == FPostId);
                    if (postCategory != null)
                    {
                        foreach(var category in postCategory)
                        {
                            context.TPostTags.Remove(category);
                        }
                    }

                    var postMsg = context.TPostMsgs.Where(n => n.FPostId == FPostId);
                    if (postMsg != null)
                    {
                        foreach (var msg in postMsg)
                        {
                            context.TPostMsgs.Remove(msg);
                        }
                    }

                    var postLikeCount = context.TPostLikeCounts.Where(n => n.FPostId == FPostId);
                    if (postLikeCount != null)
                    {
                        foreach (var like in postLikeCount)
                        {
                            context.TPostLikeCounts.Remove(like);
                        }
                    }

           
                    var postJoin = context.TJoinPeople.Where(n => n.FPostId == FPostId);
                    if (postJoin != null)
                    {
                        foreach (var join in postJoin)
                        {
                            context.TJoinPeople.Remove(join);
                        }
                    }

                    var postPhoto = context.TPostPhotos.Where(n => n.FPostId == FPostId);
                    if (postPhoto != null)
                    {
                        foreach (var photo in postPhoto)
                        {
                            context.TPostPhotos.Remove(photo);
                        }
                    }

                    var postTag = context.TPostTags.Where(n => n.FPostId == FPostId);
                    if (postTag != null)
                    {
                        foreach (var tag in postTag)
                        {
                            context.TPostTags.Remove(tag);
                        }
                    }

                   

                    TPost post = context.TPosts.FirstOrDefault(n => n.FPostId == FPostId);
                    if (post != null)
                    {
                        context.TPosts.Remove(post);
                    }
                    context.SaveChanges();
                }
                return RedirectToAction("Post_Index", "CPost");
            }
            catch
            {
                return RedirectToAction("Post_Index", "CPost");
            }
        }
        public JsonResult getAllTag()
        {
            var getTag = context.TTags.Select(n => n);
            return Json(getTag);
        }
    }
}
