namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_STT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banner", "STT", c => c.Int(nullable: false));
            AddColumn("dbo.DichVu", "STT", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DichVu", "STT");
            DropColumn("dbo.Banner", "STT");
        }
    }
}
