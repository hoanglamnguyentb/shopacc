namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_tables_1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GiaoDich", "NguoiGiaoDich");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GiaoDich", "NguoiGiaoDich", c => c.Long(nullable: false));
        }
    }
}
