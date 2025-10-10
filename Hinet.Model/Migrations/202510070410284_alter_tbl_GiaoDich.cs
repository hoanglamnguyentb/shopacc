namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbl_GiaoDich : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiaoDich", "NoiDung", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiaoDich", "NoiDung");
        }
    }
}
