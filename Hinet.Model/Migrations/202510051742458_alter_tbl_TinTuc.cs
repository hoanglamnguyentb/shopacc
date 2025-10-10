namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_TinTuc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TinTuc", "GameId", c => c.Int());
            AddColumn("dbo.TinTuc", "DichVuId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TinTuc", "DichVuId");
            DropColumn("dbo.TinTuc", "GameId");
        }
    }
}
