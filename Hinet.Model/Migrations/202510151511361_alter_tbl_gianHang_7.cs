namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_gianHang_7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHang", "NoiDungChuyenKhoan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHang", "NoiDungChuyenKhoan");
        }
    }
}
