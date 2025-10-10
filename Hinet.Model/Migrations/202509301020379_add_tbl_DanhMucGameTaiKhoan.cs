namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tbl_DanhMucGameTaiKhoan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DanhMucGameTaiKhoan",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DanhMucGameId = c.Int(nullable: false),
                        TaiKhoanId = c.Long(nullable: false),
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
            DropTable("dbo.DanhMucGameTaiKhoan");
        }
    }
}
