namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RemoveAllButSixTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GianHang",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    MoTa = c.String(),
                    TrangThai = c.String(),
                    STT = c.Int(nullable: false),
                    ViTriHienThi = c.String(),
                    Slug = c.String(),
                    AnhBia = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 256),
                    CreatedID = c.Long(),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 256),
                    UpdatedID = c.Long(),
                    IsDelete = c.Boolean(),
                    DeleteTime = c.DateTime(),
                    DeleteId = c.Long(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.VatPham",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GianHangId = c.Int(nullable: false),
                    Name = c.String(),
                    DuongDanAnh = c.String(),
                    MoTa = c.String(),
                    Slug = c.String(),
                    GiaGoc = c.Int(nullable: false),
                    STT = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 256),
                    CreatedID = c.Long(),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 256),
                    UpdatedID = c.Long(),
                    IsDelete = c.Boolean(),
                    DeleteTime = c.DateTime(),
                    DeleteId = c.Long(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ThuocTinhGianHang",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GianHangId = c.Int(nullable: false),
                    TenThuocTinh = c.String(),
                    KieuDuLieu = c.String(),
                    NhomDanhmucCode = c.String(),
                    NhomDanhMucId = c.Long(),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 256),
                    CreatedID = c.Long(),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 256),
                    UpdatedID = c.Long(),
                    IsDelete = c.Boolean(),
                    DeleteTime = c.DateTime(),
                    DeleteId = c.Long(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DonHang",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    DonHangId = c.Int(nullable: false),
                    VatPhamId = c.Int(nullable: false),
                    MaGiamGia = c.Int(nullable: false),
                    GiaGoc = c.Int(nullable: false),
                    GiaKhuyenMai = c.Int(nullable: false),
                    TrangThai = c.String(),
                    QrUrl = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 256),
                    CreatedID = c.Long(),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 256),
                    UpdatedID = c.Long(),
                    IsDelete = c.Boolean(),
                    DeleteTime = c.DateTime(),
                    DeleteId = c.Long(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DonHangGiaTriThuocTinh",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    DonHangId = c.Int(nullable: false),
                    ThuocTinhId = c.Int(),
                    ThuocTinhTxt = c.String(),
                    GiaTri = c.String(),
                    GiaTriTxt = c.String(),
                    KieuDuLieu = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 256),
                    CreatedID = c.Long(),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 256),
                    UpdatedID = c.Long(),
                    IsDelete = c.Boolean(),
                    DeleteTime = c.DateTime(),
                    DeleteId = c.Long(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.MaGiamGia",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ThongTin = c.String(),
                    GianHangApDung = c.String(),
                    ToanHeThong = c.Boolean(nullable: false),
                    SoLuong = c.Int(nullable: false),
                    TuNgay = c.DateTime(nullable: false),
                    DenNgay = c.DateTime(nullable: false),
                    TrangThai = c.Boolean(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatedBy = c.String(maxLength: 256),
                    CreatedID = c.Long(),
                    UpdatedDate = c.DateTime(nullable: false),
                    UpdatedBy = c.String(maxLength: 256),
                    UpdatedID = c.Long(),
                    IsDelete = c.Boolean(),
                    DeleteTime = c.DateTime(),
                    DeleteId = c.Long(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.MaGiamGia");
            DropTable("dbo.DonHangGiaTriThuocTinh");
            DropTable("dbo.DonHang");
            DropTable("dbo.ThuocTinhGianHang");
            DropTable("dbo.VatPham");
            DropTable("dbo.GianHang");
        }
    }
}