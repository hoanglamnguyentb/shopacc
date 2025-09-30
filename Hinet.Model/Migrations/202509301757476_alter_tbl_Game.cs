namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_Game : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Game", "STT", c => c.Int(nullable: false));
            AddColumn("dbo.Game", "ViTriHienThi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Game", "ViTriHienThi");
            DropColumn("dbo.Game", "STT");
        }
    }
}
