namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_SiteConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteConfig", "KichHoat", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteConfig", "KichHoat");
        }
    }
}
