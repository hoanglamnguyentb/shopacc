namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_SiteConfig_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteConfig", "PrimaryColor", c => c.String());
            AddColumn("dbo.SiteConfig", "SecondaryColor", c => c.String());
            AddColumn("dbo.SiteConfig", "PrimaryHoverColor", c => c.String());
            AddColumn("dbo.SiteConfig", "TextTitleColor", c => c.String());
            AddColumn("dbo.SiteConfig", "TextColor", c => c.String());
            AddColumn("dbo.SiteConfig", "LinkColor", c => c.String());
            AddColumn("dbo.SiteConfig", "LinkHoverColor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteConfig", "LinkHoverColor");
            DropColumn("dbo.SiteConfig", "LinkColor");
            DropColumn("dbo.SiteConfig", "TextColor");
            DropColumn("dbo.SiteConfig", "TextTitleColor");
            DropColumn("dbo.SiteConfig", "PrimaryHoverColor");
            DropColumn("dbo.SiteConfig", "SecondaryColor");
            DropColumn("dbo.SiteConfig", "PrimaryColor");
        }
    }
}
