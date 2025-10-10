namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_TaiKhoan_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaiKhoan", "DanhMucGameId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaiKhoan", "DanhMucGameId", c => c.String());
        }
    }
}
