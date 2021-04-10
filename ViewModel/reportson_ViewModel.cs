using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;

namespace new_layout_core.ViewModel
{
    public class reportson_ViewModel
    {
        private TArticleReportSon iv_reportson = null;
        private string UserName;
        private string UserPhoto;
        public TArticleReportSon customer { get { return iv_reportson; } }
        public reportson_ViewModel(TArticleReportSon c)
        {
            iv_reportson = c;
        }
        public reportson_ViewModel()
        {
            iv_reportson = new TArticleReportSon();
        }
        public reportson_ViewModel(TArticleReportSon c,string Name,string image)
        {
            iv_reportson = c;
            UserName = Name;
            UserPhoto = image;
        }

        public int ArticleReportSonId {
            get {
                return iv_reportson.ArticleReportSonId;
            } set {
                iv_reportson.ArticleReportSonId = value;
            }
        }
        public int? ArticleReportId
        {
            get
            {
                return iv_reportson.ArticleReportId;
            }
            set
            {
                iv_reportson.ArticleReportId = (int)value;
            }
        }
        public int? UserId
        {
            get
            {
                return iv_reportson.UserId;
            }
            set
            {
                iv_reportson.UserId = value;
            }
        }
        public string ReportTime
        {
            get
            {
                return iv_reportson.ReportTime;
            }
            set
            {
                iv_reportson.ReportTime = value;
            }
        }
        public string ReportContent
        {
            get
            {
                return iv_reportson.ReportContent;
            }
            set
            {
                iv_reportson.ReportContent = value;
            }
        }

        public string vm_UserName
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }
        public string vm_UserImage
        {
            get
            {
                return UserPhoto;
            }
            set
            {
                UserPhoto = value;
            }
        }

    }
}
