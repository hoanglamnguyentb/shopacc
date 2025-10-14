namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_MaGiamGia_6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHang", "MaGiamGiaId", c => c.Int(nullable: false));
            DropColumn("dbo.DonHang", "MaGiamGia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DonHang", "MaGiamGia", c => c.Int(nullable: false));
            DropColumn("dbo.DonHang", "MaGiamGiaId");
        }
    }
}
