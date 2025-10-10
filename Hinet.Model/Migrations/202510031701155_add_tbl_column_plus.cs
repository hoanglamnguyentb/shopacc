namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_column_plus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GiaTriThuocTinh",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TaiKhoanId = c.Int(nullable: false),
                        ThuocTinhId = c.String(),
                        ThuocTinhTxt = c.String(),
                        GiaTri = c.String(),
                        GiaTriText = c.String(),
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
            
            CreateTable(
                "dbo.ThuocTinh",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        TenThuocTinh = c.String(),
                        KieuDuLieu = c.String(),
                        DmNhomDanhmuc = c.String(),
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
            DropTable("dbo.ThuocTinh");
            DropTable("dbo.GiaTriThuocTinh");
        }
    }
}
