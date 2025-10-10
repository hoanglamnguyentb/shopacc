namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_SiteConfig : DbMigration
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
            
            DropColumn("dbo.DanhMucGame", "ThongBao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DanhMucGame", "ThongBao", c => c.String());
            DropTable("dbo.SiteConfig");
        }
    }
}
