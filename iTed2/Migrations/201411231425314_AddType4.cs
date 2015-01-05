namespace iTed2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddType4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MyFavorite");
            AddPrimaryKey("dbo.MyFavorite", new[] { "iMemberId", "VideoId" });
            DropColumn("dbo.MyFavorite", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyFavorite", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.MyFavorite");
            AddPrimaryKey("dbo.MyFavorite", "Id");
        }
    }
}
