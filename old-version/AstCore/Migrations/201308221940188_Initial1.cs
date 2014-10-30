namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.PostComments", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.NewsItems", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.NewsComments", "Tag_TagId", "dbo.Tags");
            DropIndex("dbo.Posts", new[] { "Tag_TagId" });
            DropIndex("dbo.PostComments", new[] { "Tag_TagId" });
            DropIndex("dbo.NewsItems", new[] { "Tag_TagId" });
            DropIndex("dbo.NewsComments", new[] { "Tag_TagId" });
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Post_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Post_PostId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_PostId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Post_PostId);
            
            CreateTable(
                "dbo.NewsCommentTags",
                c => new
                    {
                        NewsComment_NewsCommentId = c.Int(nullable: false),
                        Tag_TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NewsComment_NewsCommentId, t.Tag_TagId })
                .ForeignKey("dbo.NewsComments", t => t.NewsComment_NewsCommentId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .Index(t => t.NewsComment_NewsCommentId)
                .Index(t => t.Tag_TagId);
            
            CreateTable(
                "dbo.NewsItemTags",
                c => new
                    {
                        NewsItem_NewsItemId = c.Int(nullable: false),
                        Tag_TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NewsItem_NewsItemId, t.Tag_TagId })
                .ForeignKey("dbo.NewsItems", t => t.NewsItem_NewsItemId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .Index(t => t.NewsItem_NewsItemId)
                .Index(t => t.Tag_TagId);
            
            CreateTable(
                "dbo.TagPostComments",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        PostComment_PostCommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.PostComment_PostCommentId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.PostComments", t => t.PostComment_PostCommentId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.PostComment_PostCommentId);
            
            DropColumn("dbo.Posts", "Tag_TagId");
            DropColumn("dbo.PostComments", "Tag_TagId");
            DropColumn("dbo.NewsItems", "Tag_TagId");
            DropColumn("dbo.NewsComments", "Tag_TagId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsComments", "Tag_TagId", c => c.Int());
            AddColumn("dbo.NewsItems", "Tag_TagId", c => c.Int());
            AddColumn("dbo.PostComments", "Tag_TagId", c => c.Int());
            AddColumn("dbo.Posts", "Tag_TagId", c => c.Int());
            DropIndex("dbo.TagPostComments", new[] { "PostComment_PostCommentId" });
            DropIndex("dbo.TagPostComments", new[] { "Tag_TagId" });
            DropIndex("dbo.NewsItemTags", new[] { "Tag_TagId" });
            DropIndex("dbo.NewsItemTags", new[] { "NewsItem_NewsItemId" });
            DropIndex("dbo.NewsCommentTags", new[] { "Tag_TagId" });
            DropIndex("dbo.NewsCommentTags", new[] { "NewsComment_NewsCommentId" });
            DropIndex("dbo.TagPosts", new[] { "Post_PostId" });
            DropIndex("dbo.TagPosts", new[] { "Tag_TagId" });
            DropForeignKey("dbo.TagPostComments", "PostComment_PostCommentId", "dbo.PostComments");
            DropForeignKey("dbo.TagPostComments", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.NewsItemTags", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.NewsItemTags", "NewsItem_NewsItemId", "dbo.NewsItems");
            DropForeignKey("dbo.NewsCommentTags", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.NewsCommentTags", "NewsComment_NewsCommentId", "dbo.NewsComments");
            DropForeignKey("dbo.TagPosts", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.TagPosts", "Tag_TagId", "dbo.Tags");
            DropTable("dbo.TagPostComments");
            DropTable("dbo.NewsItemTags");
            DropTable("dbo.NewsCommentTags");
            DropTable("dbo.TagPosts");
            CreateIndex("dbo.NewsComments", "Tag_TagId");
            CreateIndex("dbo.NewsItems", "Tag_TagId");
            CreateIndex("dbo.PostComments", "Tag_TagId");
            CreateIndex("dbo.Posts", "Tag_TagId");
            AddForeignKey("dbo.NewsComments", "Tag_TagId", "dbo.Tags", "TagId");
            AddForeignKey("dbo.NewsItems", "Tag_TagId", "dbo.Tags", "TagId");
            AddForeignKey("dbo.PostComments", "Tag_TagId", "dbo.Tags", "TagId");
            AddForeignKey("dbo.Posts", "Tag_TagId", "dbo.Tags", "TagId");
        }
    }
}
