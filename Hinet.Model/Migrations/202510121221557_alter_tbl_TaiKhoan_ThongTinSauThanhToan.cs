namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_TaiKhoan_ThongTinSauThanhToan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiKhoan", "ThongTinSauThanhToan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaiKhoan", "ThongTinSauThanhToan");
        }
    }
}
