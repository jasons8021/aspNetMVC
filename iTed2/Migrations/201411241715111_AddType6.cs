namespace iTed2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddType6 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Category", name: "CategoryName", newName: "關鍵字");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Category", name: "關鍵字", newName: "CategoryName");
        }
    }
}
