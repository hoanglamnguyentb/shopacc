namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_DanhMucGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DanhMucGame", "NoiBat", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DanhMucGame", "NoiBat");
        }
    }
}
