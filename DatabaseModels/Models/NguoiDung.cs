using System.ComponentModel.DataAnnotations;

namespace DatabaseModels.Models;

public class NguoiDung
{
  [Key]
  public uint MaNguoiDung { get; set; }
  public required string HoTen { get; set; }
  public required string Email { get; set; }
  public required string MatKhauBam { get; set; }

  public List<HoaDon>? HoaDon { get; set; }
}
