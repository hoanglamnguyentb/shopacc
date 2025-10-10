namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_TaiKhoan_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaiKhoan", "DanhMucGameId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaiKhoan", "DanhMucGameId");
        }
    }
}
