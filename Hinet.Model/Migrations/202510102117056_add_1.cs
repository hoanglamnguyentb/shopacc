namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteConfig", "BankCode", c => c.String());
            AddColumn("dbo.SiteConfig", "AccountNumber", c => c.String());
            AddColumn("dbo.SiteConfig", "AccountName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteConfig", "AccountName");
            DropColumn("dbo.SiteConfig", "AccountNumber");
            DropColumn("dbo.SiteConfig", "BankCode");
        }
    }
}
