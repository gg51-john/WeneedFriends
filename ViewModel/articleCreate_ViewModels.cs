using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;

namespace new_layout_core.ViewModel
{
    public class articleCreate_ViewModels
    {
        private TArticle iv_article = null;
        public TArticle article { get { return iv_article; } }
        public articleCreate_ViewModels(TArticle a)
        {
            iv_article = a;
        }
        public articleCreate_ViewModels()
        {
            iv_article = new TArticle();
        }

        public int ArticleId { 
            get {return iv_article.ArticleId;} 
            set {iv_article.ArticleId=value;} 
        }
        public int? ArticleCategoryId
        {
            get { return iv_article.ArticleCategoryId; }
            set { iv_article.ArticleCategoryId = value; }
        }
        public int? UserId
        {
            get { return iv_article.UserId; }
            set { iv_article.UserId = value; }
        }
        public string ArtTitle
        {
            get { return iv_article.ArtTitle; }
            set { iv_article.ArtTitle = value; }
        }
        public string ArtContent
        {
            get { return iv_article.ArtContent; }
            set { iv_article.ArtContent = value; }
        }
        public string Date {
            get { return iv_article.Date; }
            set { iv_article.Date = value; }
        }
        public string Image
        {
            get { return iv_article.Image; }
            set { iv_article.Image = value; }
        }
        public IFormFile photo
        {
            get;set;
        }
    }
}
