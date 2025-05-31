using DatabaseModels.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModels;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<NguoiDung> NguoiDung { get; set; }
  public DbSet<HoaDon> HoaDon { get; set; }

  public DbSet<Phim> Phim { get; set; }
  public DbSet<Phim_TheLoai> Phim_TheLoai { get; set; }
  public DbSet<TheLoai> TheLoai { get; set; }

  public DbSet<CaChieu> CaChieu { get; set; }
  public DbSet<LoaiHinh> LoaiHinh { get; set; }

  public DbSet<Ghe> Ghe { get; set; }
  public DbSet<LoaiGhe> LoaiGhe { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    var hoaDonTable = modelBuilder.Entity<HoaDon>();
    hoaDonTable.HasOne(h => h.NguoiDung).WithMany(n => n.HoaDon).HasForeignKey(h => h.MaNguoiDung);

    var gheTable = modelBuilder.Entity<Ghe>();
    gheTable.HasIndex(g => new { g.Hang, g.Cot, g.MaCaChieu }).IsUnique(true);
    gheTable.HasOne(g => g.LoaiGhe).WithMany(l => l.Ghe).HasForeignKey(g => g.MaLoaiGhe);
    gheTable.HasOne(g => g.CaChieu).WithMany(c => c.Ghe).HasForeignKey(g => g.MaCaChieu);
    gheTable.HasOne(g => g.HoaDon).WithMany(h => h.Ghe).HasForeignKey(g => g.MaHoaDon);

    var caChieuTable = modelBuilder.Entity<CaChieu>();
    caChieuTable.HasOne(c => c.Phim).WithMany(p => p.CaChieu).HasForeignKey(c => c.MaPhim);
    caChieuTable.HasOne(c => c.LoaiHinh).WithMany(l => l.CaChieu).HasForeignKey(c => c.MaLoaiHinh);

    var phim_theloaiTable = modelBuilder.Entity<Phim_TheLoai>();
    phim_theloaiTable.HasIndex(p => new { p.MaPhim, p.MaTheLoai }).IsUnique(true);
    phim_theloaiTable.HasOne(p => p.Phim).WithMany(p => p.P_TL).HasForeignKey(p => p.MaPhim);
    phim_theloaiTable.HasOne(p => p.TheLoai).WithMany(p => p.P_TL).HasForeignKey(p => p.MaTheLoai);

    // modelBuilder.Entity<CaChieu>().HasOne(p => p.Phim).WithMany(p => p.CaChieu).HasForeignKey(c => c.MaPhim);
    // modelBuilder.Entity<Phim>().HasIndex(p => p.TenPhim).IsUnique(true);

    // var veSchema = modelBuilder.Entity<Ghe>();
    // veSchema.HasIndex(i => new { i.MaCaChieu, i.TenGhe }).IsUnique(true);
    // veSchema.HasOne(v => v.NguoiDung).WithMany(n => n.Ve).HasForeignKey(v => v.MaNguoiDung);
    // veSchema.HasOne(v => v.CaChieu).WithMany(c => c.Ve).HasForeignKey(v => v.MaCaChieu);

    base.OnModelCreating(modelBuilder);
  }
}