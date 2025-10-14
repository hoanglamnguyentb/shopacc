namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_MaGiamGia_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaGiamGia", "KieuGiam", c => c.String(maxLength: 20));
            AddColumn("dbo.MaGiamGia", "GiaTriGiam", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaGiamGia", "GiaTriGiam");
            DropColumn("dbo.MaGiamGia", "KieuGiam");
        }
    }
}
