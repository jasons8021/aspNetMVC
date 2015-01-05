namespace iTed2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddType2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MyFavorite", "iMemberId", "dbo.iMember");
            DropForeignKey("dbo.MyFavorite", "VideoId", "dbo.Video");
            DropIndex("dbo.MyFavorite", new[] { "iMemberId" });
            DropIndex("dbo.MyFavorite", new[] { "VideoId" });
            DropTable("dbo.MyFavorite");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MyFavorite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        iMemberId = c.Int(nullable: false),
                        VideoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.MyFavorite", "VideoId");
            CreateIndex("dbo.MyFavorite", "iMemberId");
            AddForeignKey("dbo.MyFavorite", "VideoId", "dbo.Video", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MyFavorite", "iMemberId", "dbo.iMember", "Id", cascadeDelete: true);
        }
    }
}
