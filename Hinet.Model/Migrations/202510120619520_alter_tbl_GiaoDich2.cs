namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_GiaoDich2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiaoDich", "MaGiaoDich", c => c.String());
            AddColumn("dbo.GiaoDich", "MaGiaoDichDoiTac", c => c.String());
            AddColumn("dbo.GiaoDich", "NoiDungChuyenKhoan", c => c.String());
            AddColumn("dbo.GiaoDich", "WebhookTrangThai", c => c.String());
            AddColumn("dbo.GiaoDich", "ThoiGianWebhook", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiaoDich", "ThoiGianWebhook");
            DropColumn("dbo.GiaoDich", "WebhookTrangThai");
            DropColumn("dbo.GiaoDich", "NoiDungChuyenKhoan");
            DropColumn("dbo.GiaoDich", "MaGiaoDichDoiTac");
            DropColumn("dbo.GiaoDich", "MaGiaoDich");
        }
    }
}
