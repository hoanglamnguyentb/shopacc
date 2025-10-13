namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbls : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GianHang", "LuuY", c => c.String());
            AlterColumn("dbo.MaGiamGia", "ToanHeThong", c => c.Boolean());
            AlterColumn("dbo.MaGiamGia", "SoLuong", c => c.Int());
            AlterColumn("dbo.MaGiamGia", "TuNgay", c => c.DateTime());
            AlterColumn("dbo.MaGiamGia", "DenNgay", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaGiamGia", "DenNgay", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MaGiamGia", "TuNgay", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MaGiamGia", "SoLuong", c => c.Int(nullable: false));
            AlterColumn("dbo.MaGiamGia", "ToanHeThong", c => c.Boolean(nullable: false));
            DropColumn("dbo.GianHang", "LuuY");
        }
    }
}
