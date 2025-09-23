namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_TaiKhoan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaiKhoan",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        GameId = c.Int(nullable: false),
                        TrangThai = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        GiaGoc = c.Int(nullable: false),
                        GiaKhuyenMai = c.Int(nullable: false),
                        Mota = c.String(),
                        ViTri = c.Int(nullable: false),
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
            DropTable("dbo.TaiKhoan");
        }
    }
}
