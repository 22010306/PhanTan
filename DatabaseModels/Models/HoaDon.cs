using System.ComponentModel.DataAnnotations;

namespace DatabaseModels.Models;

public class HoaDon
{
  [Key]
  public uint MaHoaDon { get; set; }
  public DateTime ThoiGianDat { get; set; }

  public required uint MaNguoiDung { get; set; }
  public NguoiDung NguoiDung { get; set; } = null!;

  public List<Ghe>? Ghe { get; set; }
}