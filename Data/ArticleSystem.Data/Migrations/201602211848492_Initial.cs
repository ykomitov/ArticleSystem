namespace ArticleSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        TextHtml = c.String(nullable: false),
                        HeaderImage = c.Binary(),
                        HeaderImageFileName = c.String(),
                        SliderImage = c.Binary(),
                        SliderImageFileName = c.String(),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.ArticleCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.CategoryId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(),
                        AuthorId = c.String(maxLength: 128),
                        ArticleId = c.Int(nullable: false),
                        ParentCommentId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .Index(t => t.AuthorId)
                .Index(t => t.ArticleId)
                .Index(t => t.ParentCommentId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteType = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Articles", "CategoryId", "dbo.ArticleCategories");
            DropForeignKey("dbo.Articles", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Votes", new[] { "IsDeleted" });
            DropIndex("dbo.Votes", new[] { "UserId" });
            DropIndex("dbo.Votes", new[] { "ArticleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Articles", new[] { "IsDeleted" });
            DropIndex("dbo.Articles", new[] { "CategoryId" });
            DropIndex("dbo.Articles", new[] { "AuthorId" });
            DropIndex("dbo.ArticleCategories", new[] { "IsDeleted" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Votes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleCategories");
        }
    }
}
