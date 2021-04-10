using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using new_layout_core.Models;

namespace new_layout_core.ViewModel
{
    public class selectarticle_ViewModel
    {
        private TArticle iv_article = null;
        private int like;
        private int respones;
        public TArticle customer { get { return iv_article; } }
        public selectarticle_ViewModel(TArticle c)
        {
            iv_article = c;
        }
        public selectarticle_ViewModel()
        {
            iv_article = new TArticle();
        }
        public selectarticle_ViewModel(TArticle c,int likecount,int responcount)
        {
            iv_article = c;
            like = likecount;
            respones = responcount;
        }

        public int ArticleId { 
            get {
                return iv_article.ArticleId;
            }
            set {
                iv_article.ArticleId = value;
            } }
        public string ArtTitle
        {
            get
            {
                return iv_article.ArtTitle;
            }
            set
            {
                iv_article.ArtTitle = value;
            }
        }
        public string Date
        {
            get
            {
                return iv_article.Date;
            }
            set
            {
                iv_article.Date = value;
            }
        }
        public int Like
        {
            get
            {
                return like;
            }
            set
            {
                like = value;
            }
        }
        public int respone
        {
            get
            {
                return respones;
            }
            set
            {
                respones = value;
            }
        }

    }
}
