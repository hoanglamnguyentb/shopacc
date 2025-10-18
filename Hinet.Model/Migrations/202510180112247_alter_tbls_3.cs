namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbls_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GianHang", "KichHoat", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GianHang", "KichHoat");
        }
    }
}
