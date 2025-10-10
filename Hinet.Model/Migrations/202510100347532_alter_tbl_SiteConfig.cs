namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_SiteConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteConfig", "ThongBao", c => c.String());
            AddColumn("dbo.SiteConfig", "MoTa", c => c.String());
            AddColumn("dbo.SiteConfig", "LinkFacebook", c => c.String());
            AddColumn("dbo.SiteConfig", "SoDienThoai", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteConfig", "SoDienThoai");
            DropColumn("dbo.SiteConfig", "LinkFacebook");
            DropColumn("dbo.SiteConfig", "MoTa");
            DropColumn("dbo.SiteConfig", "ThongBao");
        }
    }
}
