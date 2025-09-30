namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_GiaoDich : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GiaoDich",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        DoiTuongId = c.Long(nullable: false),
                        LoaiDoiTuong = c.String(),
                        LoaiGiaoDich = c.String(),
                        TrangThai = c.String(),
                        PhuongThucThanhToan = c.String(),
                        NgayGiaoDich = c.DateTime(nullable: false),
                        NgayThanhToan = c.DateTime(),
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
            DropTable("dbo.GiaoDich");
        }
    }
}
