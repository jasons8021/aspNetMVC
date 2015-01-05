namespace iTed2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddType3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyFavorite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        iMemberId = c.Int(nullable: false),
                        VideoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.iMember", t => t.iMemberId, cascadeDelete: true)
                .ForeignKey("dbo.Video", t => t.VideoId, cascadeDelete: true)
                .Index(t => t.iMemberId)
                .Index(t => t.VideoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyFavorite", "VideoId", "dbo.Video");
            DropForeignKey("dbo.MyFavorite", "iMemberId", "dbo.iMember");
            DropIndex("dbo.MyFavorite", new[] { "VideoId" });
            DropIndex("dbo.MyFavorite", new[] { "iMemberId" });
            DropTable("dbo.MyFavorite");
        }
    }
}
