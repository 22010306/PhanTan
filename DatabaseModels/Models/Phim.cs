using System.ComponentModel.DataAnnotations;

namespace DatabaseModels.Models;

public class Phim
{
  [Key]
  public uint MaPhim { get; set; }
  public required string TenPhim { get; set; }
  public required uint DoDai { get; set; }
  public required float DanhGia { get; set; }

  public List<CaChieu>? CaChieu { get; set; }
  public List<Phim_TheLoai>? P_TL { get; set; }
}

public class Phim_TheLoai
{
  [Key]
  public uint Id { get; set; }

  public required uint MaPhim { get; set; }
  public Phim Phim { get; set; } = null!;

  public required uint MaTheLoai { get; set; }
  public TheLoai TheLoai { get; set; } = null!;
}

public class TheLoai
{
  [Key]
  public uint MaTheLoai { get; set; }
  public required string TenTheLoai { get; set; }

  public List<Phim_TheLoai>? P_TL { get; set; }
}