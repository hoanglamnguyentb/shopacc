namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeposit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deposit",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    UserId = c.Long(nullable: false),
                    Code = c.String(),
                    Amount = c.Long(nullable: false),
                    Status = c.String(),
                    Expiry = c.DateTime(nullable: false),
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
            DropTable("dbo.Deposit");
        }
    }
}
