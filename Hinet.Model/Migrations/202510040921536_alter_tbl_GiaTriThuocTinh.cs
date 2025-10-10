namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_GiaTriThuocTinh : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TaiKhoan");
            DropPrimaryKey("dbo.ThuocTinh");
            AlterColumn("dbo.GiaTriThuocTinh", "ThuocTinhId", c => c.Int());
            AlterColumn("dbo.TaiKhoan", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ThuocTinh", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TaiKhoan", "Id");
            AddPrimaryKey("dbo.ThuocTinh", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ThuocTinh");
            DropPrimaryKey("dbo.TaiKhoan");
            AlterColumn("dbo.ThuocTinh", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.TaiKhoan", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.GiaTriThuocTinh", "ThuocTinhId", c => c.String());
            AddPrimaryKey("dbo.ThuocTinh", "Id");
            AddPrimaryKey("dbo.TaiKhoan", "Id");
        }
    }
}
