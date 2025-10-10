namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBalanceToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUser", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue:0));
        }

        public override void Down()
        {
            DropColumn("dbo.AppUser", "Balance");
        }
    }
}
