namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_TaiKhoan_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaiKhoan", "GiaKhuyenMai", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaiKhoan", "GiaKhuyenMai", c => c.Int(nullable: false));
        }
    }
}
