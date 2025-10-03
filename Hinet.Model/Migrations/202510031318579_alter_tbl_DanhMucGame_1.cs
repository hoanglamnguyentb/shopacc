namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_DanhMucGame_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DanhMucGame", "ThongBao", c => c.String());
            AddColumn("dbo.DanhMucGame", "LaLoaiDongGia", c => c.Boolean());
            AddColumn("dbo.DanhMucGame", "GiaGoc", c => c.Int(nullable: false));
            AddColumn("dbo.DanhMucGame", "GiaKhuyenMai", c => c.Int());
            DropTable("dbo.SiteConfig");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SiteConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Keywords = c.String(),
                        OgTitle = c.String(),
                        OgDescription = c.String(),
                        OgImage = c.String(),
                        SiteTitle = c.String(),
                        Favicon = c.String(),
                        Logo = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedID = c.Long(),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedID = c.Long(),
                        IsDelete = c.Boolean(),
                        DeleteTime = c.DateTime(),
                        DeleteId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.DanhMucGame", "GiaKhuyenMai");
            DropColumn("dbo.DanhMucGame", "GiaGoc");
            DropColumn("dbo.DanhMucGame", "LaLoaiDongGia");
            DropColumn("dbo.DanhMucGame", "ThongBao");
        }
    }
}
