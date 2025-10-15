namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_gianHang_8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHang", "MaGiaoDich", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHang", "MaGiaoDich");
        }
    }
}
