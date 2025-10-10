namespace Hinet.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audit",
                c => new
                    {
                        AuditID = c.Guid(nullable: false),
                        SessionID = c.String(),
                        IPAddress = c.String(),
                        UserName = c.String(),
                        URLAccessed = c.String(),
                        TimeAccessed = c.DateTime(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.AuditID);
            
            CreateTable(
                "dbo.Banner",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        DuongDanAnh = c.String(),
                        Link = c.String(),
                        KichHoat = c.Boolean(),
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
                "dbo.BinhLuan",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NguoiBinhLuanId = c.Long(nullable: false),
                        DoiTuongId = c.Long(nullable: false),
                        LoaiDoiTuong = c.String(),
                        NoiDung = c.String(),
                        Diem = c.Int(nullable: false),
                        ParentId = c.Long(nullable: false),
                        TrangThai = c.String(),
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
                "dbo.DanhMucGame",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        Name = c.String(),
                        DuongDanAnh = c.String(),
                        MoTa = c.String(),
                        Slug = c.String(),
                        ThongBao = c.String(),
                        LaLoaiDongGia = c.Boolean(),
                        GiaGoc = c.Int(nullable: false),
                        GiaKhuyenMai = c.Int(),
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
                "dbo.DanhMucGameTaiKhoan",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DanhMucGameId = c.Int(nullable: false),
                        TaiKhoanId = c.Long(nullable: false),
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
                "dbo.Deposit",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        Code = c.String(),
                        Amount = c.Long(nullable: false),
                        Status = c.String(),
                        Expiry = c.DateTime(nullable: false),
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
                "dbo.DichVu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        DuongDanAnh = c.String(),
                        Link = c.String(),
                        KichHoat = c.Boolean(),
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
                "dbo.DM_DulieuDanhmuc",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GroupId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 250),
                        Code = c.String(nullable: false, maxLength: 250),
                        Note = c.String(maxLength: 500),
                        Priority = c.Int(),
                        Icon = c.String(),
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
                "dbo.DM_NhomDanhmuc",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GroupName = c.String(nullable: false, maxLength: 150),
                        GroupCode = c.String(nullable: false, maxLength: 150),
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
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        MoTa = c.String(),
                        TrangThai = c.String(),
                        STT = c.Int(nullable: false),
                        ViTriHienThi = c.String(),
                        Slug = c.String(),
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
                "dbo.GiaoDich",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        DoiTuongId = c.Long(nullable: false),
                        LoaiDoiTuong = c.String(),
                        LoaiGiaoDich = c.String(),
                        TrangThai = c.String(),
                        PhuongThucThanhToan = c.String(),
                        NgayGiaoDich = c.DateTime(nullable: false),
                        NgayThanhToan = c.DateTime(),
                        SoTien = c.Int(nullable: false),
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
                "dbo.GiaTriThuocTinh",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TaiKhoanId = c.Int(nullable: false),
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
                "dbo.Module",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 250),
                        Name = c.String(nullable: false, maxLength: 250),
                        Order = c.Int(nullable: false),
                        IsShow = c.Boolean(nullable: false),
                        Icon = c.String(),
                        ClassCss = c.String(maxLength: 250),
                        StyleCss = c.String(maxLength: 250),
                        Link = c.String(),
                        AllowFilterScope = c.Boolean(),
                        IsMobile = c.Boolean(),
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
                "dbo.Notification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        Link = c.String(),
                        FromUser = c.Long(),
                        ToUser = c.Long(),
                        IsRead = c.Boolean(nullable: false),
                        Type = c.String(maxLength: 250),
                        Param = c.String(),
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
                "dbo.Operation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ModuleId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                        URL = c.String(nullable: false, maxLength: 250),
                        Code = c.String(nullable: false),
                        Css = c.String(maxLength: 250),
                        IsShow = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                        Icon = c.String(),
                        UrlFull = c.String(),
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
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Code = c.String(nullable: false, maxLength: 250),
                        ConfigRequestId = c.Long(nullable: false),
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
                "dbo.RoleOperation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        OperationId = c.Long(nullable: false),
                        IsAccess = c.Int(nullable: false),
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
                "dbo.AppRole",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AppUserRole",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AppRole", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AppUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SiteConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Keywords = c.String(),
                        OgTitle = c.String(),
                        OgUrl = c.String(),
                        OgDescription = c.String(),
                        OgImage = c.String(),
                        SiteTitle = c.String(),
                        Favicon = c.String(),
                        Logo = c.String(),
                        KichHoat = c.Boolean(),
                        PrimaryColor = c.String(),
                        SecondaryColor = c.String(),
                        PrimaryHoverColor = c.String(),
                        TextTitleColor = c.String(),
                        TextColor = c.String(),
                        LinkColor = c.String(),
                        LinkHoverColor = c.String(),
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
                "dbo.TaiKhoan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        GameId = c.Int(nullable: false),
                        DanhMucGameId = c.Int(),
                        TrangThai = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        GiaGoc = c.Int(nullable: false),
                        GiaKhuyenMai = c.Int(),
                        Mota = c.String(),
                        ViTri = c.Int(nullable: false),
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
                "dbo.TaiLieuDinhKem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenTaiLieu = c.String(nullable: false, maxLength: 500),
                        LoaiTaiLieu = c.String(maxLength: 250),
                        Item_ID = c.Long(),
                        MoTa = c.String(),
                        DuongDanFile = c.String(),
                        DinhDangFile = c.String(maxLength: 250),
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
                "dbo.ThuocTinh",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
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
                "dbo.TinTuc",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Slug = c.String(),
                        TieuDe = c.String(),
                        NoiDung = c.String(),
                        AnhBia = c.String(),
                        TacGia = c.String(),
                        TrangThai = c.String(),
                        ThoiGianXuatBan = c.DateTime(nullable: false),
                        GameId = c.Int(),
                        DichVuId = c.Int(),
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
                "dbo.UserOperation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        OperationId = c.Long(nullable: false),
                        IsAccess = c.Int(nullable: false),
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
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
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
                "dbo.AppUser",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(maxLength: 256),
                        PhoneNumber = c.String(),
                        BirthDay = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        Address = c.String(),
                        FullName = c.String(maxLength: 250),
                        Avatar = c.String(),
                        TypeAccount = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedID = c.Long(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                        UpdatedID = c.Long(),
                        IsDelete = c.Boolean(),
                        DeleteTime = c.DateTime(),
                        DeleteId = c.Long(),
                        Detail = c.String(),
                        LastLogin = c.DateTime(),
                        Block = c.Boolean(),
                        IsSendMail = c.Boolean(nullable: false),
                        ErrorMessage = c.Boolean(nullable: false),
                        Token = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AppClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AppLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AppUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUserRole", "UserId", "dbo.AppUser");
            DropForeignKey("dbo.AppLogin", "UserId", "dbo.AppUser");
            DropForeignKey("dbo.AppClaim", "UserId", "dbo.AppUser");
            DropForeignKey("dbo.AppUserRole", "RoleId", "dbo.AppRole");
            DropIndex("dbo.AppLogin", new[] { "UserId" });
            DropIndex("dbo.AppClaim", new[] { "UserId" });
            DropIndex("dbo.AppUser", "UserNameIndex");
            DropIndex("dbo.AppUserRole", new[] { "RoleId" });
            DropIndex("dbo.AppUserRole", new[] { "UserId" });
            DropIndex("dbo.AppRole", "RoleNameIndex");
            DropTable("dbo.AppLogin");
            DropTable("dbo.AppClaim");
            DropTable("dbo.AppUser");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserOperation");
            DropTable("dbo.TinTuc");
            DropTable("dbo.ThuocTinh");
            DropTable("dbo.TaiLieuDinhKem");
            DropTable("dbo.TaiKhoan");
            DropTable("dbo.SiteConfig");
            DropTable("dbo.AppUserRole");
            DropTable("dbo.AppRole");
            DropTable("dbo.RoleOperation");
            DropTable("dbo.Role");
            DropTable("dbo.Operation");
            DropTable("dbo.Notification");
            DropTable("dbo.Module");
            DropTable("dbo.GiaTriThuocTinh");
            DropTable("dbo.GiaoDich");
            DropTable("dbo.Game");
            DropTable("dbo.DM_NhomDanhmuc");
            DropTable("dbo.DM_DulieuDanhmuc");
            DropTable("dbo.DichVu");
            DropTable("dbo.Deposit");
            DropTable("dbo.DanhMucGameTaiKhoan");
            DropTable("dbo.DanhMucGame");
            DropTable("dbo.BinhLuan");
            DropTable("dbo.Banner");
            DropTable("dbo.Audit");
        }
    }
}
