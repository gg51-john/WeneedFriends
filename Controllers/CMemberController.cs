using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;
using new_layout_core.ViewModel;

namespace Test.Controllers
{

    public class CMemberController : Controller
    {
        public JsonResult getPost(int user)
        {
            var post = new WeNeedFriendsFINContext().TPosts.Where(u => u.FUserId == user).Select(n => new CCalenderViewModel
            {
                FPostId = n.FPostId,
                FTitle = n.FTitle,
                FPostTime = n.FPostTime,
                FPeople = n.FPeople,
                FPostAddress = n.FPostAddress,
                FPostCity = n.FPostCity,
                FPostDistrict = n.FPostDistrict,
                joinpeople = n.TJoinPeople.Where(m => m.FUserId == m.FUserId).Count(),
                FDescription = n.FDescription,
                FSportName = n.FSportName,
                FUserName = n.FUser.FUserName
            }).ToList();
            return Json(post);
        }

        public IActionResult ForgetPassWord()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }



        public IActionResult memberdata()
        {
            return View();
        }

        public IActionResult Activity()
        {
            return View();
        }
        public IActionResult Article()
        {
            return View();
        }

    }
}
