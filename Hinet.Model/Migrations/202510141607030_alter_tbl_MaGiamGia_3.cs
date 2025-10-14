namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_MaGiamGia_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHang", "SoLuong", c => c.Int(nullable: false));
            AddColumn("dbo.DonHang", "TongTien", c => c.Int(nullable: false));
            AddColumn("dbo.DonHang", "GhiChu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHang", "GhiChu");
            DropColumn("dbo.DonHang", "TongTien");
            DropColumn("dbo.DonHang", "SoLuong");
        }
    }
}
