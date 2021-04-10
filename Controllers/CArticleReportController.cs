using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;
using new_layout_core.ViewModel;

namespace Test.Controllers
{
    public class CArticleReportController : Controller
    {
        WeNeedFriendsFINContext db = new WeNeedFriendsFINContext();
        public IActionResult Index()
        {
            return View();
        }
        public bool sendreport(int articleid,int userid,string message,string date)
        {
            if (message!=null&&date!=null&&articleid!=0&&userid!=0)
            {
                TArticleReport report = new TArticleReport();
                report.ArticleId = articleid;
                report.UserId = userid;
                report.ArticleReport = message;
                report.ArticleReportTime = date;
                db.TArticleReports.Add(report);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public IActionResult getreport(int articleid)
        {
            var q = from r in db.TArticleReports
                    where r.ArticleId == articleid
                    select r;
            List < TArticleReport > list = q.ToList();
            if (list.Count == 0)
            {
                list = null;
            }
            
            return PartialView("pv_report", list);
        }
        public IActionResult getreportson(string reportid)
        {
            int id;
            bool flag = int.TryParse(reportid,out id);
            List<new_layout_core.ViewModel.reportson_ViewModel> list = new List<reportson_ViewModel>();
            if (flag)
            {
                var q = from r in db.TArticleReportSons
                        where r.ArticleReportId == id
                        select new { all=r,UserName=r.User.FUserName ,UserImage=r.User.FUserPhoto};
                
                if (q.ToList().Count != 0)
                {
                    foreach (var item in q)
                    {
                        list.Add(new reportson_ViewModel(item.all, item.UserName,item.UserImage));
                    }
                }
                else
                {
                    list = null;
                }
            }
            else
            {
                list = null;
            }
            return PartialView("pv_report_son", list);
        }

        public JsonResult sendreportson(int userid,int reportid,string message,string date)
        {
            if (userid != 0 && reportid != 0 && message != null)
            {
                TArticleReportSon son = new TArticleReportSon();
                son.UserId = userid;
                son.ArticleReportId = reportid;
                son.ReportContent = message;
                son.ReportTime = date;
                db.TArticleReportSons.Add(son);
                db.SaveChanges();
                return Json(new { result = true });
            }
            else
            {
                return Json(new { result = false });
            }
        }
        public JsonResult allreportcounts(int articleid)
        {
            var q = (from i in db.TArticleReports
                    where i.ArticleId == articleid
                     select i).Count();
           
            var q2 = (from m in db.TArticleReports
                    join s in db.TArticleReportSons on m.ArticleReportId equals s.ArticleReportId
                    where m.ArticleId == articleid
                      select m).Count();

            return Json(new { result = q+q2 });
        }
        public JsonResult GetthisReportsUserName(int reportid)
        {
            var q = (from n in db.TArticleReports
                    where n.ArticleReportId == reportid
                    select new { name=n.User.FUserName,image=n.User.FUserPhoto}).FirstOrDefault();
            return Json(new { result = q });
        }
        public JsonResult GetthisReportsSonUserName(int reportsonid)
        {
            var q = (from n in db.TArticleReportSons
                     where n.ArticleReportSonId == reportsonid
                     select new { name = n.User.FUserName, image = n.User.FUserPhoto }).FirstOrDefault();
            return Json(new { result = q });
        }
    }
}
