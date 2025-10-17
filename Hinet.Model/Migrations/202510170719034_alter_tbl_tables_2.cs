namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_tables_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiaoDich", "NguoiGiaoDich", c => c.Long(nullable: false));
            AddColumn("dbo.GiaoDich", "NgayXuLy", c => c.DateTime());
            DropColumn("dbo.GiaoDich", "UserId");
            DropColumn("dbo.GiaoDich", "NgayThanhToan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GiaoDich", "NgayThanhToan", c => c.DateTime());
            AddColumn("dbo.GiaoDich", "UserId", c => c.Long(nullable: false));
            DropColumn("dbo.GiaoDich", "NgayXuLy");
            DropColumn("dbo.GiaoDich", "NguoiGiaoDich");
        }
    }
}
