namespace iTed2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddType5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Video", "ThumbnailUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Video", "ThumbnailUrl");
        }
    }
}
