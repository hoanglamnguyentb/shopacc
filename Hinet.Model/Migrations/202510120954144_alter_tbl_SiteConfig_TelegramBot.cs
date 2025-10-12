namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_SiteConfig_TelegramBot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteConfig", "TelegramBotToken", c => c.String());
            AddColumn("dbo.SiteConfig", "TelegramChatId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteConfig", "TelegramChatId");
            DropColumn("dbo.SiteConfig", "TelegramBotToken");
        }
    }
}
