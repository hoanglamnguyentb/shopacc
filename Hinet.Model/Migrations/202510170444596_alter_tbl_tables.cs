namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_tables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiaoDich", "NguoiGiaoDich", c => c.Long(nullable: false));
            AddColumn("dbo.ThuocTinhGianHang", "IsRequired", c => c.Boolean());
            AddColumn("dbo.ThuocTinhGianHang", "PlaceHolder", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThuocTinhGianHang", "PlaceHolder");
            DropColumn("dbo.ThuocTinhGianHang", "IsRequired");
            DropColumn("dbo.GiaoDich", "NguoiGiaoDich");
        }
    }
}
