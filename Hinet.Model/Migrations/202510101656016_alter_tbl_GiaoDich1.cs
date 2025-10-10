namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_GiaoDich1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiaoDich", "TenTaiKhoanCanNap", c => c.String());
            AddColumn("dbo.GiaoDich", "MatKhauTaiKhoanNap", c => c.String());
            AddColumn("dbo.GiaoDich", "SoTienNap", c => c.Int(nullable: false));
            AddColumn("dbo.GiaoDich", "GhiChu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiaoDich", "GhiChu");
            DropColumn("dbo.GiaoDich", "SoTienNap");
            DropColumn("dbo.GiaoDich", "MatKhauTaiKhoanNap");
            DropColumn("dbo.GiaoDich", "TenTaiKhoanCanNap");
        }
    }
}
