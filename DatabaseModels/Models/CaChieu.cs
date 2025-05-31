using System.ComponentModel.DataAnnotations;

namespace DatabaseModels.Models;

public class CaChieu
{
  [Key]
  public uint MaCaChieu { get; set; }
  public required DateTime ThoiGianChieu { get; set; }
  public required uint SoHang { get; set; }
  public required uint SoCot { get; set; }

  public uint MaPhim { get; set; }
  public Phim Phim { get; set; } = null!;

  public uint MaLoaiHinh;
  public LoaiHinh LoaiHinh { get; set; } = null!;

  public List<Ghe>? Ghe { get; set; }
}

public class LoaiHinh
{
  [Key]
  public uint MaLoaiHinh { get; set; }
  public required string TenLoaiHinh { get; set; }

  public List<CaChieu>? CaChieu { get; set; }
}