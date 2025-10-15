namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_gianHang_9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DonHang", "MaGiamGiaId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DonHang", "MaGiamGiaId", c => c.Int(nullable: false));
        }
    }
}
