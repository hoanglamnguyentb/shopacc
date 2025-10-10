namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_slug : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DanhMucGame", "Slug", c => c.String());
            AddColumn("dbo.Game", "Slug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Game", "Slug");
            DropColumn("dbo.DanhMucGame", "Slug");
        }
    }
}
