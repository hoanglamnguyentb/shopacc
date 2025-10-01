namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_BinhLuan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BinhLuan",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NguoiBinhLuanId = c.Long(nullable: false),
                        DoiTuongId = c.Long(nullable: false),
                        LoaiDoiTuong = c.String(),
                        NoiDung = c.String(),
                        Diem = c.Int(nullable: false),
                        ParentId = c.Long(nullable: false),
                        TrangThai = c.String(),
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
            DropTable("dbo.BinhLuan");
        }
    }
}
