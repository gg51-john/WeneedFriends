using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using new_layout_core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace new_layout_core.ViewModel
{
    public class CPostViewModel
    {
        public int FPostId { get; set; }
        public int FUserId { get; set; }
        [DisplayName("活動名稱")]
        [Required]
        public string FTitle { get; set; }
        [DisplayName("活動類型")]
        [Required]
        public string FSportName { get; set; }
        [DisplayName("活動描述")]
        [Required]
        public string FDescription { get; set; }
        [DisplayName("活動日期")]
        [Required]
        public string FPostTime { get; set; }
        [DisplayName("參加人數")]
        [Required]
        public int FPeople { get; set; }
        public int FLikeCount { get; set; }
        public string FSystemTime { get; set; }
        [DisplayName("市")]
        [Required]
        public string FPostCity { get; set; }
        [DisplayName("區")]
        [Required]
        public string FPostDistrict { get; set; }
        [DisplayName("詳細地址")]
        [Required]
        public string FPostAddress { get; set; }
        public bool FPostCheck { get; set; }
        public int joinpeople { get; set; }
        public IEnumerable<TPostPhoto> photo { get; set; }
        public List<IFormFile> image { get; set; }
    }
}
