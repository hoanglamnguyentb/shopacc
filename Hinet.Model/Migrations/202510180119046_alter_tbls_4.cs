namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_tbls_4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GianHang", "TrangThai");
            DropColumn("dbo.GianHang", "ViTriHienThi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GianHang", "ViTriHienThi", c => c.String());
            AddColumn("dbo.GianHang", "TrangThai", c => c.String());
        }
    }
}
