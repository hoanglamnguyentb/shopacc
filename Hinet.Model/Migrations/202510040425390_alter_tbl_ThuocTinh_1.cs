namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_ThuocTinh_1 : DbMigration
    {
        public override void Up()
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
            
            AddColumn("dbo.ThuocTinh", "NhomDanhmucCode", c => c.String());
            AddColumn("dbo.ThuocTinh", "NhomDanhMucId", c => c.Long());
            DropColumn("dbo.ThuocTinh", "DmNhomDanhmuc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ThuocTinh", "DmNhomDanhmuc", c => c.String());
            DropColumn("dbo.ThuocTinh", "NhomDanhMucId");
            DropColumn("dbo.ThuocTinh", "NhomDanhmucCode");
            DropTable("dbo.SiteConfig");
        }
    }
}
