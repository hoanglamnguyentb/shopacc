namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_GiaTriThuocTinh_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiaTriThuocTinh", "GiaTriTxt", c => c.String());
            AddColumn("dbo.GiaTriThuocTinh", "KieuDuLieu", c => c.String());
            DropColumn("dbo.GiaTriThuocTinh", "GiaTriText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GiaTriThuocTinh", "GiaTriText", c => c.String());
            DropColumn("dbo.GiaTriThuocTinh", "KieuDuLieu");
            DropColumn("dbo.GiaTriThuocTinh", "GiaTriTxt");
        }
    }
}
