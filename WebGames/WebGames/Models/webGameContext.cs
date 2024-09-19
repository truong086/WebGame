using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebGames.Models
{
    public partial class webGameContext : DbContext
    {
        public webGameContext()
        {
        }

        public webGameContext(DbContextOptions<webGameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Congtycungcap> Congtycungcaps { get; set; } = null!;
        public virtual DbSet<Noinhap> Noinhaps { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetai> OrderDetais { get; set; } = null!;
        public virtual DbSet<Phieunhap> Phieunhaps { get; set; } = null!;
        public virtual DbSet<Phieuxuat> Phieuxuats { get; set; } = null!;
        public virtual DbSet<Sanpham> Sanphams { get; set; } = null!;
        public virtual DbSet<Sualoi> Sualois { get; set; } = null!;
        public virtual DbSet<Takhoan> Takhoans { get; set; } = null!;
        public virtual DbSet<Theloai> Theloais { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("User Id=sa;Password=1234;Server=LAPTOP-D5OKMO3H\\SQLEXPRESS;Database=webGame");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Congtycungcap>(entity =>
            {
                entity.ToTable("congtycungcap");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(500)
                    .HasColumnName("diachi");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .HasColumnName("email");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ten)
                    .HasMaxLength(250)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<Noinhap>(entity =>
            {
                entity.ToTable("noinhap");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ten)
                    .HasMaxLength(200)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Statuc).HasColumnName("statuc");

                entity.Property(e => e.Ten)
                    .HasMaxLength(100)
                    .HasColumnName("ten");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__orders__users_id__6E01572D");
            });

            modelBuilder.Entity<OrderDetai>(entity =>
            {
                entity.ToTable("orderDetais");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.SanphamId).HasColumnName("sanpham_id");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Tongtien).HasColumnName("tongtien");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetais)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__orderDeta__order__3C69FB99");

                entity.HasOne(d => d.Sanpham)
                    .WithMany(p => p.OrderDetais)
                    .HasForeignKey(d => d.SanphamId)
                    .HasConstraintName("FK__orderDeta__sanph__3D5E1FD2");
            });

            modelBuilder.Entity<Phieunhap>(entity =>
            {
                entity.ToTable("phieunhap");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ngaynhap)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaynhap");

                entity.Property(e => e.NguoinhapId).HasColumnName("nguoinhap_id");

                entity.Property(e => e.NoinhapId).HasColumnName("noinhap_id");

                entity.Property(e => e.SanphamId).HasColumnName("sanpham_id");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Tongtien).HasColumnName("tongtien");

                entity.HasOne(d => d.Nguoinhap)
                    .WithMany(p => p.Phieunhaps)
                    .HasForeignKey(d => d.NguoinhapId)
                    .HasConstraintName("FK__phieunhap__nguoi__2EDAF651");

                entity.HasOne(d => d.Noinhap)
                    .WithMany(p => p.Phieunhaps)
                    .HasForeignKey(d => d.NoinhapId)
                    .HasConstraintName("FK__phieunhap__noinh__2DE6D218");

                entity.HasOne(d => d.Sanpham)
                    .WithMany(p => p.Phieunhaps)
                    .HasForeignKey(d => d.SanphamId)
                    .HasConstraintName("FK__phieunhap__sanph__2CF2ADDF");
            });

            modelBuilder.Entity<Phieuxuat>(entity =>
            {
                entity.ToTable("phieuxuat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CongtycungcapId).HasColumnName("congtycungcap_id");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(500)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Ngaycap)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaycap");

                entity.Property(e => e.NguoicapId).HasColumnName("nguoicap_id");

                entity.Property(e => e.SanphamId).HasColumnName("sanpham_id");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Tongtien).HasColumnName("tongtien");

                entity.HasOne(d => d.Congtycungcap)
                    .WithMany(p => p.Phieuxuats)
                    .HasForeignKey(d => d.CongtycungcapId)
                    .HasConstraintName("FK__phieuxuat__congt__32AB8735");

                entity.HasOne(d => d.Nguoicap)
                    .WithMany(p => p.Phieuxuats)
                    .HasForeignKey(d => d.NguoicapId)
                    .HasConstraintName("FK__phieuxuat__nguoi__339FAB6E");

                entity.HasOne(d => d.Sanpham)
                    .WithMany(p => p.Phieuxuats)
                    .HasForeignKey(d => d.SanphamId)
                    .HasConstraintName("FK__phieuxuat__sanph__31B762FC");
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.ToTable("sanpham");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gia).HasColumnName("gia");

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(500)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Noidung)
                    .HasMaxLength(500)
                    .HasColumnName("noidung");

                entity.Property(e => e.Solanclick).HasColumnName("solanclick");

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");

                entity.Property(e => e.TheloaiId).HasColumnName("theloai_id");

                entity.HasOne(d => d.Theloai)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.TheloaiId)
                    .HasConstraintName("FK__sanpham__theloai__47DBAE45");
            });

            modelBuilder.Entity<Sualoi>(entity =>
            {
                entity.ToTable("sualoi");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ten)
                    .HasMaxLength(100)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<Takhoan>(entity =>
            {
                entity.ToTable("takhoan");

                entity.HasIndex(e => e.Tentaikhoan, "UQ__takhoan__8ADCB8A7C737903E")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__takhoan__AB6E616473B1DB63")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Matkhau)
                    .HasMaxLength(50)
                    .HasColumnName("matkhau");

                entity.Property(e => e.Phanquyen).HasColumnName("phanquyen");

                entity.Property(e => e.Tentaikhoan)
                    .HasMaxLength(50)
                    .HasColumnName("tentaikhoan");
            });

            modelBuilder.Entity<Theloai>(entity =>
            {
                entity.ToTable("theloai");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
