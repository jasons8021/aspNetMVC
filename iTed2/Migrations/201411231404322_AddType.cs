namespace iTed2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyFavorite", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MyFavorite", "Id");
            CreateIndex("dbo.MyFavorite", "iMemberId");
            AddForeignKey("dbo.MyFavorite", "iMemberId", "dbo.iMember", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyFavorite", "MemberId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MyFavorite", new[] { "MemberId", "VideoId" });
            CreateIndex("dbo.MyFavorite", "iMember_Id");
            AddForeignKey("dbo.MyFavorite", "iMember_Id", "dbo.iMember", "Id");
        }
    }
}
