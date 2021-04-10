using System;
using System.Collections.Generic;

#nullable disable

namespace new_layout_core.Models
{
    public partial class Member
    {
        public Member()
        {
            TArticleChecks = new HashSet<TArticleCheck>();
            TArticleLikes = new HashSet<TArticleLike>();
            TArticleReportSonChecks = new HashSet<TArticleReportSonCheck>();
            TArticleReportSons = new HashSet<TArticleReportSon>();
            TArticleReports = new HashSet<TArticleReport>();
            TArticleStores = new HashSet<TArticleStore>();
            TArticles = new HashSet<TArticle>();
            TMemberTags = new HashSet<TMemberTag>();
            TPosts = new HashSet<TPost>();
            TmyBuyDetails = new HashSet<TmyBuyDetail>();
        }

        public int FUserId { get; set; }
        public string FEmail { get; set; }
        public string FPassword { get; set; }
        public string FUserName { get; set; }
        public string FGender { get; set; }
        public string FUserBirthday { get; set; }
        public string FUserPhone { get; set; }
        public string FUserPhoto { get; set; }
        public string FCity { get; set; }
        public string FDistrict { get; set; }
        public string FAddress { get; set; }
        public string FJoinTime { get; set; }
        public int? Fstatus { get; set; }
        public string Fsalt { get; set; }

        public virtual ICollection<TArticleCheck> TArticleChecks { get; set; }
        public virtual ICollection<TArticleLike> TArticleLikes { get; set; }
        public virtual ICollection<TArticleReportSonCheck> TArticleReportSonChecks { get; set; }
        public virtual ICollection<TArticleReportSon> TArticleReportSons { get; set; }
        public virtual ICollection<TArticleReport> TArticleReports { get; set; }
        public virtual ICollection<TArticleStore> TArticleStores { get; set; }
        public virtual ICollection<TArticle> TArticles { get; set; }
        public virtual ICollection<TMemberTag> TMemberTags { get; set; }
        public virtual ICollection<TPost> TPosts { get; set; }
        public virtual ICollection<TmyBuyDetail> TmyBuyDetails { get; set; }
    }
}
