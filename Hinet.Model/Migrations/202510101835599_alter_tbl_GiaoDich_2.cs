namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_GiaoDich_2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GiaoDich", "SoTienNap");
            DropColumn("dbo.GiaoDich", "GhiChu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GiaoDich", "GhiChu", c => c.String());
            AddColumn("dbo.GiaoDich", "SoTienNap", c => c.Int(nullable: false));
        }
    }
}
