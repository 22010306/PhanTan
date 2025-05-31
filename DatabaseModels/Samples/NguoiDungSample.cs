using DatabaseModels.Models;

namespace DatabaseModels.Samples;

public static class NguoiDungSample
{
  public static List<NguoiDung> Data { get; } = [
    new() { Email="a", HoTen="Test1", MatKhauBam="a" },
    new() { Email="b", HoTen="Test2", MatKhauBam="a" },
    new() { Email="c", HoTen="Test3", MatKhauBam="a" },
  ];
}