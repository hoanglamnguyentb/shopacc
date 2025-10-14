namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_MaGiamGia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaGiamGia", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaGiamGia", "Code");
        }
    }
}
