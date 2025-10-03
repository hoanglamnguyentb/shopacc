using Hinet.Model.Entities;
using Hinet.Model.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace Hinet.Model
{
	public class DbContext : IdentityDbContext<AppUser, AppRole, long, AppLogin, AppUserRole, AppClaim>
	{
		public DbContext()
			: base("Name=HinetContext")
		{
			//sử dụng cho việc unit test
			Database.SetInitializer<DbContext>(null);
		}

		public DbSet<Audit> Audit { get; set; }
		public DbSet<Module> Module { get; set; }
		public DbSet<Operation> Operation { get; set; }
		public DbSet<Role> Role { get; set; }
		public DbSet<RoleOperation> RoleOperation { get; set; }
		public DbSet<DM_DulieuDanhmuc> DM_DulieuDanhmuc { get; set; }
		public DbSet<DM_NhomDanhmuc> DM_NhomDanhmuc { get; set; }
		public DbSet<TaiLieuDinhKem> TaiLieuDinhKem { get; set; }
		public DbSet<UserRole> UserRole { get; set; }
		public DbSet<Notification> Notification { get; set; }
		public DbSet<UserOperation> UserOperation { get; set; }
		public DbSet<Game> Game { get; set; }
		public DbSet<DanhMucGame> DanhMucGame { get; set; }
		public DbSet<DanhMucGameTaiKhoan> DanhMucGameTaiKhoan { get; set; }
		public DbSet<TaiKhoan> TaiKhoan { get; set; }
		public DbSet<ThuocTinh> ThuocTinh { get; set; }
		public DbSet<GiaTriThuocTinh> GiaTriThuocTinh { get; set; }
		public DbSet<DichVu> DichVu { get; set; }
		public DbSet<Banner> Banner { get; set; }
		public DbSet<TinTuc> TinTuc { get; set; }
		public DbSet<GiaoDich> GiaoDich { get; set; }
		public DbSet<BinhLuan> BinhLuan { get; set; }

		public static DbContext Create()
		{
			return new DbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<AppUser>().ToTable("AppUser");
			modelBuilder.Entity<AppUserRole>().ToTable("AppUserRole");
			modelBuilder.Entity<AppRole>().ToTable("AppRole");
			modelBuilder.Entity<AppClaim>().ToTable("AppClaim");
			modelBuilder.Entity<AppLogin>().ToTable("AppLogin");
			modelBuilder.Entity<AppUser>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			modelBuilder.Entity<AppRole>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			modelBuilder.Entity<AppClaim>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
		}

		public override int SaveChanges()
		{
			var modifiedEntries = ChangeTracker.Entries()
				.Where(x => x.Entity is IAuditableEntity
					&& (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

			foreach (var entry in modifiedEntries)
			{
				IAuditableEntity entity = entry.Entity as IAuditableEntity;
				if (entity != null)
				{
					string identityName = Thread.CurrentPrincipal.Identity.Name;
					var userId = this.Users.Where(x => x.UserName == identityName).Select(x => x.Id).FirstOrDefault();

					DateTime now = DateTime.Now;

					if (entry.State == System.Data.Entity.EntityState.Added)
					{
						entity.CreatedBy = identityName;
						entity.CreatedDate = now;
						entity.CreatedID = userId;
					}
					else
					{
						base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
						base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
					}
					entity.UpdatedBy = identityName;
					entity.UpdatedDate = now;
					entity.UpdatedID = userId;
				}
			}

			return base.SaveChanges();
		}
	}
}