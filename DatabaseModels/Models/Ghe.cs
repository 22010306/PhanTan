using System.ComponentModel.DataAnnotations;

namespace DatabaseModels.Models;

public class Ghe
{
  [Key]
  public uint MaGhe { get; set; }
  public uint Hang { get; set; }
  public uint Cot { get; set; }

  public required uint MaLoaiGhe { get; set; }
  public LoaiGhe LoaiGhe { get; set; } = null!;

  public required uint MaCaChieu { get; set; }
  public CaChieu CaChieu { get; set; } = null!;

  public required uint MaHoaDon { get; set; }
  public HoaDon? HoaDon { get; set; }
}

public class LoaiGhe
{
  [Key]
  public uint MaLoaiGhe { get; set; }
  public string TenLoaiGhe { get; set; } = null!;
  public ulong GiaTien { get; set; }

  public List<Ghe>? Ghe { get; set; }
}