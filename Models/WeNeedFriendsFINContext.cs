using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace new_layout_core.Models
{
    public partial class WeNeedFriendsFINContext : DbContext
    {
        public WeNeedFriendsFINContext()
        {
        }

        public WeNeedFriendsFINContext(DbContextOptions<WeNeedFriendsFINContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChangeStatusDate> ChangeStatusDates { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<PresetPhoto> PresetPhotos { get; set; }
        public virtual DbSet<TArticle> TArticles { get; set; }
        public virtual DbSet<TArticleCategory> TArticleCategories { get; set; }
        public virtual DbSet<TArticleCheck> TArticleChecks { get; set; }
        public virtual DbSet<TArticleLike> TArticleLikes { get; set; }
        public virtual DbSet<TArticleReport> TArticleReports { get; set; }
        public virtual DbSet<TArticleReportCheck> TArticleReportChecks { get; set; }
        public virtual DbSet<TArticleReportSon> TArticleReportSons { get; set; }
        public virtual DbSet<TArticleReportSonCheck> TArticleReportSonChecks { get; set; }
        public virtual DbSet<TArticleStore> TArticleStores { get; set; }
        public virtual DbSet<TArticleTag> TArticleTags { get; set; }
        public virtual DbSet<TCategory> TCategories { get; set; }
        public virtual DbSet<TCity> TCities { get; set; }
        public virtual DbSet<TJoinPerson> TJoinPeople { get; set; }
        public virtual DbSet<TLocation> TLocations { get; set; }
        public virtual DbSet<TMemberTag> TMemberTags { get; set; }
        public virtual DbSet<TPost> TPosts { get; set; }
        public virtual DbSet<TPostLikeCount> TPostLikeCounts { get; set; }
        public virtual DbSet<TPostMsg> TPostMsgs { get; set; }
        public virtual DbSet<TPostMsgReport> TPostMsgReports { get; set; }
        public virtual DbSet<TPostMsged> TPostMsgeds { get; set; }
        public virtual DbSet<TPostMsgedReport> TPostMsgedReports { get; set; }
        public virtual DbSet<TPostPhoto> TPostPhotos { get; set; }
        public virtual DbSet<TPostReport> TPostReports { get; set; }
        public virtual DbSet<TPostStore> TPostStores { get; set; }
        public virtual DbSet<TPostTag> TPostTags { get; set; }
        public virtual DbSet<TProduct> TProducts { get; set; }
        public virtual DbSet<TProductCategory> TProductCategories { get; set; }
        public virtual DbSet<TProductPic> TProductPics { get; set; }
        public virtual DbSet<TSizeId> TSizeIds { get; set; }
        public virtual DbSet<TSport> TSports { get; set; }
        public virtual DbSet<TTag> TTags { get; set; }
        public virtual DbSet<TmyBuyDetail> TmyBuyDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WeNeedFriendsFIN;Integrated Security=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<ChangeStatusDate>(entity =>
            {
                entity.HasKey(e => e.Changeid);

                entity.ToTable("change_status_date");

                entity.Property(e => e.Changeid).HasColumnName("changeid");

                entity.Property(e => e.Recoverytime).HasColumnName("recoverytime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.FUserId);

                entity.ToTable("Member");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.Property(e => e.FAddress).HasColumnName("fAddress");

                entity.Property(e => e.FCity)
                    .HasMaxLength(30)
                    .HasColumnName("fCity");

                entity.Property(e => e.FDistrict)
                    .HasMaxLength(30)
                    .HasColumnName("fDistrict");

                entity.Property(e => e.FEmail)
                    .HasMaxLength(50)
                    .HasColumnName("fEmail");

                entity.Property(e => e.FGender)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("fGender")
                    .IsFixedLength(true);

                entity.Property(e => e.FJoinTime)
                    .HasMaxLength(50)
                    .HasColumnName("fJoinTime");

                entity.Property(e => e.FPassword).HasColumnName("fPassword");

                entity.Property(e => e.FUserBirthday)
                    .HasMaxLength(50)
                    .HasColumnName("fUserBirthday");

                entity.Property(e => e.FUserName)
                    .HasMaxLength(10)
                    .HasColumnName("fUserName")
                    .IsFixedLength(true);

                entity.Property(e => e.FUserPhone)
                    .HasMaxLength(50)
                    .HasColumnName("fUserPhone");

                entity.Property(e => e.FUserPhoto).HasColumnName("fUserPhoto");

                entity.Property(e => e.Fsalt).HasColumnName("fsalt");

                entity.Property(e => e.Fstatus).HasColumnName("fstatus");
            });

            modelBuilder.Entity<PresetPhoto>(entity =>
            {
                entity.ToTable("PresetPhoto");

                entity.Property(e => e.PresetPhotoId).HasColumnName("PresetPhotoID");

                entity.Property(e => e.PresetPhoto1).HasColumnName("PresetPhoto");
            });

            modelBuilder.Entity<TArticle>(entity =>
            {
                entity.HasKey(e => e.ArticleId)
                    .HasName("PK_Article");

                entity.ToTable("tArticle");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.ArticleCategoryId).HasColumnName("ArticleCategoryID");

                entity.Property(e => e.Date).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ArticleCategory)
                    .WithMany(p => p.TArticles)
                    .HasForeignKey(d => d.ArticleCategoryId)
                    .HasConstraintName("FK_Article_ArticleCategory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TArticles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Article_Member");
            });

            modelBuilder.Entity<TArticleCategory>(entity =>
            {
                entity.HasKey(e => e.ArticleCategoryId)
                    .HasName("PK_ArticleCategory_1");

                entity.ToTable("tArticleCategory");

                entity.Property(e => e.ArticleCategoryId).HasColumnName("ArticleCategoryID");

                entity.Property(e => e.ArticleCategoryName).HasMaxLength(20);
            });

            modelBuilder.Entity<TArticleCheck>(entity =>
            {
                entity.HasKey(e => e.CheckReportId)
                    .HasName("PK_CheckReport");

                entity.ToTable("tArticleCheck");

                entity.Property(e => e.CheckReportId).HasColumnName("CheckReportID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.TArticleChecks)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_CheckReport_Article");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TArticleChecks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_CheckReport_Member");
            });

            modelBuilder.Entity<TArticleLike>(entity =>
            {
                entity.HasKey(e => e.LikeCountId)
                    .HasName("PK_LikeCount");

                entity.ToTable("tArticleLike");

                entity.Property(e => e.LikeCountId).HasColumnName("LikeCountID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.TArticleLikes)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_LikeCount_Article");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TArticleLikes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_LikeCount_Member");
            });

            modelBuilder.Entity<TArticleReport>(entity =>
            {
                entity.HasKey(e => e.ArticleReportId)
                    .HasName("PK_Article_Report");

                entity.ToTable("tArticleReport");

                entity.Property(e => e.ArticleReportId).HasColumnName("ArticleReportID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.ArticleReport).HasMaxLength(50);

                entity.Property(e => e.ArticleReportTime)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.TArticleReports)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Article_Report_Article");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TArticleReports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Article_Report_Member");
            });

            modelBuilder.Entity<TArticleReportCheck>(entity =>
            {
                entity.HasKey(e => e.UserCheckreportId)
                    .HasName("PK_UserCheckreport");

                entity.ToTable("tArticleReportCheck");

                entity.Property(e => e.UserCheckreportId).HasColumnName("UserCheckreportID");

                entity.Property(e => e.ArticleReportId).HasColumnName("ArticleReportID");

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ArticleReport)
                    .WithMany(p => p.TArticleReportChecks)
                    .HasForeignKey(d => d.ArticleReportId)
                    .HasConstraintName("FK_UserCheckreport_Article_Report");
            });

            modelBuilder.Entity<TArticleReportSon>(entity =>
            {
                entity.HasKey(e => e.ArticleReportSonId);

                entity.ToTable("tArticleReportSon");

                entity.Property(e => e.ArticleReportSonId).HasColumnName("ArticleReportSonID");

                entity.Property(e => e.ArticleReportId).HasColumnName("ArticleReportID");

                entity.Property(e => e.ReportTime).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ArticleReport)
                    .WithMany(p => p.TArticleReportSons)
                    .HasForeignKey(d => d.ArticleReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tArticleReportSon_tArticleReport");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TArticleReportSons)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tArticleReportSon_Member");
            });

            modelBuilder.Entity<TArticleReportSonCheck>(entity =>
            {
                entity.HasKey(e => e.ReportSonCheckId);

                entity.ToTable("tArticleReportSonCheck");

                entity.Property(e => e.ReportSonCheckId).HasColumnName("ReportSonCheckID");

                entity.Property(e => e.ArticleReportSonId).HasColumnName("ArticleReportSonID");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ArticleReportSon)
                    .WithMany(p => p.TArticleReportSonChecks)
                    .HasForeignKey(d => d.ArticleReportSonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tArticleReportSonCheck_tArticleReportSon");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TArticleReportSonChecks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tArticleReportSonCheck_Member");
            });

            modelBuilder.Entity<TArticleStore>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK_Article_Store");

                entity.ToTable("tArticleStore");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.TArticleStores)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_Article_Store_Article");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TArticleStores)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Article_Store_Member");
            });

            modelBuilder.Entity<TArticleTag>(entity =>
            {
                entity.HasKey(e => e.ArticleTagId);

                entity.ToTable("tArticleTag");

                entity.Property(e => e.ArticleTagId).HasColumnName("ArticleTagID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.TArticleTags)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK_tArticleTag_tArticle");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TArticleTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_tArticleTag_tTag");
            });

            modelBuilder.Entity<TCategory>(entity =>
            {
                entity.HasKey(e => e.FCategoryId)
                    .HasName("PK_SportCategory");

                entity.ToTable("tCategory");

                entity.Property(e => e.FCategoryId).HasColumnName("fCategoryID");

                entity.Property(e => e.FCategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("fCategoryName");
            });

            modelBuilder.Entity<TCity>(entity =>
            {
                entity.HasKey(e => e.FCityId)
                    .HasName("PK_City");

                entity.ToTable("tCity");

                entity.Property(e => e.FCityId).HasColumnName("fCityID");

                entity.Property(e => e.FCityName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("fCityName");
            });

            modelBuilder.Entity<TJoinPerson>(entity =>
            {
                entity.HasKey(e => e.FJoinId)
                    .HasName("PK_JoinPeople");

                entity.ToTable("tJoinPeople");

                entity.Property(e => e.FJoinId).HasColumnName("fJoinID");

                entity.Property(e => e.FJoinTime)
                    .HasMaxLength(50)
                    .HasColumnName("fJoinTime");

                entity.Property(e => e.FJoincheck).HasColumnName("fJoincheck");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FPost)
                    .WithMany(p => p.TJoinPeople)
                    .HasForeignKey(d => d.FPostId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_JoinPeople_Post");
            });

            modelBuilder.Entity<TLocation>(entity =>
            {
                entity.HasKey(e => e.FLocationId)
                    .HasName("PK_PostLocal");

                entity.ToTable("tLocation");

                entity.Property(e => e.FLocationId).HasColumnName("fLocationID");

                entity.Property(e => e.FCityId).HasColumnName("fCityID");

                entity.Property(e => e.FDistrictName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("fDistrictName");

                entity.HasOne(d => d.FCity)
                    .WithMany(p => p.TLocations)
                    .HasForeignKey(d => d.FCityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Local_City");
            });

            modelBuilder.Entity<TMemberTag>(entity =>
            {
                entity.HasKey(e => e.MemberTagId);

                entity.ToTable("tMemberTag");

                entity.Property(e => e.MemberTagId).HasColumnName("MemberTagID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TMemberTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tMemberTag_tTag");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TMemberTags)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tMemberTag_Member");
            });

            modelBuilder.Entity<TPost>(entity =>
            {
                entity.HasKey(e => e.FPostId)
                    .HasName("PK_Post_1");

                entity.ToTable("tPost");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FDescription)
                    .IsRequired()
                    .HasColumnName("fDescription");

                entity.Property(e => e.FLikeCount)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fLikeCount")
                    .HasDefaultValueSql("(N'0')");

                entity.Property(e => e.FPeople).HasColumnName("fPeople");

                entity.Property(e => e.FPostAddress).HasColumnName("fPostAddress");

                entity.Property(e => e.FPostCheck).HasColumnName("fPostCheck");

                entity.Property(e => e.FPostCity)
                    .HasMaxLength(10)
                    .HasColumnName("fPostCIty");

                entity.Property(e => e.FPostDistrict)
                    .HasMaxLength(10)
                    .HasColumnName("fPostDistrict");

                entity.Property(e => e.FPostTime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fPostTime");

                entity.Property(e => e.FSportName)
                    .HasMaxLength(50)
                    .HasColumnName("fSportName");

                entity.Property(e => e.FSystemTime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fSystemTime");

                entity.Property(e => e.FTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("fTitle");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FUser)
                    .WithMany(p => p.TPosts)
                    .HasForeignKey(d => d.FUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tPost_Member");
            });

            modelBuilder.Entity<TPostLikeCount>(entity =>
            {
                entity.HasKey(e => e.FPostLikeId)
                    .HasName("PK_Post_LikeCount");

                entity.ToTable("tPost_LikeCount");

                entity.Property(e => e.FPostLikeId).HasColumnName("fPostLikeID");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FPost)
                    .WithMany(p => p.TPostLikeCounts)
                    .HasForeignKey(d => d.FPostId)
                    .HasConstraintName("FK_Post_LikeCount_Post");
            });

            modelBuilder.Entity<TPostMsg>(entity =>
            {
                entity.HasKey(e => e.FPostMsgId)
                    .HasName("PK_PostScore");

                entity.ToTable("tPostMsg");

                entity.Property(e => e.FPostMsgId).HasColumnName("fPostMsgID");

                entity.Property(e => e.FMsgDesc).HasColumnName("fMsgDesc");

                entity.Property(e => e.FMsgModify)
                    .HasColumnName("fMsgModify")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FMsgModifyTime)
                    .HasMaxLength(50)
                    .HasColumnName("fMsgModifyTime");

                entity.Property(e => e.FMsgTime)
                    .HasMaxLength(50)
                    .HasColumnName("fMsgTime");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FStart).HasColumnName("fStart");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FPost)
                    .WithMany(p => p.TPostMsgs)
                    .HasForeignKey(d => d.FPostId)
                    .HasConstraintName("FK_PostScore_Post");
            });

            modelBuilder.Entity<TPostMsgReport>(entity =>
            {
                entity.HasKey(e => e.FPostMsgReportId);

                entity.ToTable("tPostMsgReport");

                entity.Property(e => e.FPostMsgReportId).HasColumnName("fPostMsgReportID");

                entity.Property(e => e.FMsgReportDesc)
                    .HasMaxLength(50)
                    .HasColumnName("fMsgReportDesc");

                entity.Property(e => e.FPostMsgId).HasColumnName("fPostMsgID");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FPostMsg)
                    .WithMany(p => p.TPostMsgReports)
                    .HasForeignKey(d => d.FPostMsgId)
                    .HasConstraintName("FK_tPostMsgReport_tPostMsg");
            });

            modelBuilder.Entity<TPostMsged>(entity =>
            {
                entity.HasKey(e => e.FPostMsgedId);

                entity.ToTable("tPostMsged");

                entity.Property(e => e.FPostMsgedId).HasColumnName("fPostMsgedID");

                entity.Property(e => e.FMsgedDesc).HasColumnName("fMsgedDesc");

                entity.Property(e => e.FMsgedModify).HasColumnName("fMsgedModify");

                entity.Property(e => e.FMsgedModifyTime)
                    .HasMaxLength(50)
                    .HasColumnName("fMsgedModifyTime");

                entity.Property(e => e.FMsgedTime)
                    .HasMaxLength(50)
                    .HasColumnName("fMsgedTime");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FPostMsgId).HasColumnName("fPostMsgID");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FPostMsg)
                    .WithMany(p => p.TPostMsgeds)
                    .HasForeignKey(d => d.FPostMsgId)
                    .HasConstraintName("FK_tPostMsged_tPostMsg");
            });

            modelBuilder.Entity<TPostMsgedReport>(entity =>
            {
                entity.HasKey(e => e.FPostMsgedReportId);

                entity.ToTable("tPostMsgedReport");

                entity.Property(e => e.FPostMsgedReportId).HasColumnName("fPostMsgedReportID");

                entity.Property(e => e.FMsgedReportDesc)
                    .HasMaxLength(50)
                    .HasColumnName("fMsgedReportDesc");

                entity.Property(e => e.FPostMsgedId).HasColumnName("fPostMsgedID");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FPostMsged)
                    .WithMany(p => p.TPostMsgedReports)
                    .HasForeignKey(d => d.FPostMsgedId)
                    .HasConstraintName("FK_tPostMsgedReport_tPostMsged");
            });

            modelBuilder.Entity<TPostPhoto>(entity =>
            {
                entity.HasKey(e => e.FPostPhotoId)
                    .HasName("PK_PostPhoto");

                entity.ToTable("tPostPhoto");

                entity.Property(e => e.FPostPhotoId).HasColumnName("fPostPhotoID");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FPostPhoto).HasColumnName("fPostPhoto");

                entity.HasOne(d => d.FPost)
                    .WithMany(p => p.TPostPhotos)
                    .HasForeignKey(d => d.FPostId)
                    .HasConstraintName("FK_tPostPhoto_tPost");
            });

            modelBuilder.Entity<TPostReport>(entity =>
            {
                entity.HasKey(e => e.FPostReportId);

                entity.ToTable("tPostReport");

                entity.Property(e => e.FPostReportId).HasColumnName("fPostReportID");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FPostReportDesc)
                    .HasMaxLength(50)
                    .HasColumnName("fPostReportDesc");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FPost)
                    .WithMany(p => p.TPostReports)
                    .HasForeignKey(d => d.FPostId)
                    .HasConstraintName("FK_tPostReport_tPost");
            });

            modelBuilder.Entity<TPostStore>(entity =>
            {
                entity.HasKey(e => e.FPostStoreId);

                entity.ToTable("tPostStore");

                entity.Property(e => e.FPostStoreId).HasColumnName("fPostStoreID");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FPost)
                    .WithMany(p => p.TPostStores)
                    .HasForeignKey(d => d.FPostId)
                    .HasConstraintName("FK_tPostStore_tPost");
            });

            modelBuilder.Entity<TPostTag>(entity =>
            {
                entity.HasKey(e => e.FPosTagId)
                    .HasName("PK_tPostCategory");

                entity.ToTable("tPostTag");

                entity.Property(e => e.FPosTagId).HasColumnName("fPosTagID");

                entity.Property(e => e.FPostId).HasColumnName("fPostID");

                entity.Property(e => e.FTagId).HasColumnName("fTagID");

                entity.HasOne(d => d.FPost)
                    .WithMany(p => p.TPostTags)
                    .HasForeignKey(d => d.FPostId)
                    .HasConstraintName("FK_tPostTag_tPost");

                entity.HasOne(d => d.FTag)
                    .WithMany(p => p.TPostTags)
                    .HasForeignKey(d => d.FTagId)
                    .HasConstraintName("FK_tPostTag_tTag");
            });

            modelBuilder.Entity<TProduct>(entity =>
            {
                entity.HasKey(e => e.FProductId);

                entity.ToTable("tProduct");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.Property(e => e.FProductCategory).HasColumnName("fProductCategory");

                entity.Property(e => e.FProductDescription).HasColumnName("fProductDescription");

                entity.Property(e => e.FProductName)
                    .HasMaxLength(50)
                    .HasColumnName("fProductName");

                entity.Property(e => e.FProductPrice).HasColumnName("fProductPrice");

                entity.Property(e => e.FProductQty).HasColumnName("fProductQty");

                entity.Property(e => e.FSize)
                    .HasMaxLength(50)
                    .HasColumnName("fSize");
            });

            modelBuilder.Entity<TProductCategory>(entity =>
            {
                entity.HasKey(e => e.FCategoryId)
                    .HasName("PK_tCategory");

                entity.ToTable("tProductCategory");

                entity.Property(e => e.FCategoryId).HasColumnName("fCategoryID");

                entity.Property(e => e.FCategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("fCategoryName");
            });

            modelBuilder.Entity<TProductPic>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("tProductPic");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.Property(e => e.FProductPicPath)
                    .HasMaxLength(255)
                    .HasColumnName("fProductPicPath");
            });

            modelBuilder.Entity<TSizeId>(entity =>
            {
                entity.HasKey(e => e.FSizeId);

                entity.ToTable("tSizeID");

                entity.Property(e => e.FSizeId).HasColumnName("fSizeID");

                entity.Property(e => e.FSizeName)
                    .HasMaxLength(50)
                    .HasColumnName("fSizeName");
            });

            modelBuilder.Entity<TSport>(entity =>
            {
                entity.HasKey(e => e.FSportId)
                    .HasName("PK_Sport");

                entity.ToTable("tSport");

                entity.Property(e => e.FSportId).HasColumnName("fSportID");

                entity.Property(e => e.FSportName)
                    .HasMaxLength(50)
                    .HasColumnName("fSportName");
            });

            modelBuilder.Entity<TTag>(entity =>
            {
                entity.HasKey(e => e.TagId);

                entity.ToTable("tTag");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TmyBuyDetail>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("TMyBuyDetail");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.FBuytime)
                    .HasMaxLength(50)
                    .HasColumnName("fBuytime");

                entity.Property(e => e.FProductId).HasColumnName("fProductID");

                entity.Property(e => e.FProductName)
                    .HasMaxLength(50)
                    .HasColumnName("fProductName");

                entity.Property(e => e.FProductPrice).HasColumnName("fProductPrice");

                entity.Property(e => e.FProductQty).HasColumnName("fProductQty");

                entity.Property(e => e.FProductTotal).HasColumnName("fProductTotal");

                entity.Property(e => e.FState)
                    .HasMaxLength(50)
                    .HasColumnName("fState");

                entity.Property(e => e.FUserId).HasColumnName("fUserID");

                entity.HasOne(d => d.FUser)
                    .WithMany(p => p.TmyBuyDetails)
                    .HasForeignKey(d => d.FUserId)
                    .HasConstraintName("FK_TMyBuyDetail_Member");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
