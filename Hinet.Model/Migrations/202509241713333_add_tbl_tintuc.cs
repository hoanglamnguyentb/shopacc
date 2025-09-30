namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_tintuc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TinTuc",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Slug = c.String(),
                        TieuDe = c.String(),
                        NoiDung = c.String(),
                        AnhBia = c.String(),
                        TacGia = c.String(),
                        TrangThai = c.String(),
                        ThoiGianXuatBan = c.DateTime(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TinTuc");
        }
    }
}
