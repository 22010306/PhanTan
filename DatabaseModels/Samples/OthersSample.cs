using DatabaseModels.Models;

namespace DatabaseModels.Samples;

public static class OthersSample
{
  public static List<Phim_TheLoai> GeneratePhim_TheLoai(Phim phim, List<TheLoai> theLoai, int count = 5)
  {
    var random = new Random();
    int times = random.Next(2, 5);
    List<uint> temp = [];
    List<Phim_TheLoai> result = [];
    for (int i = 0; i < times; i++)
    {
      TheLoai t;
      do t = theLoai[random.Next(theLoai.Count)];
      while (temp.Contains(t.MaTheLoai));

      temp.Add(t.MaTheLoai);
      result.Add(new() { MaPhim = phim.MaPhim, MaTheLoai = t.MaTheLoai });
    }
    return result;
  }

  public static List<CaChieu> GenerateCaChieu(Phim phim, List<LoaiHinh> loaiHinhList)
  {
    var random = new Random();
    uint length = phim.DoDai + 20;

    List<CaChieu> result = [];
    DateTime date = DateTime.UtcNow.Date.AddMinutes(length);
    uint t = (uint)Math.Ceiling(24d * 60 / length);
    for (int j = 0; j < t; j++)
    {
      date = date.AddMinutes(length);
      CaChieu CaChieu = new()
      {
        ThoiGianChieu = DateTime.SpecifyKind(date, DateTimeKind.Utc),
        MaPhim = phim.MaPhim,
        MaLoaiHinh = loaiHinhList[random.Next(loaiHinhList.Count)].MaLoaiHinh,
        SoHang = (uint)random.Next(7, 10),
        SoCot = (uint)random.Next(8, 12),
      };
      result.Add(CaChieu);
    }
    return result;
  }

  public static List<HoaDon> GenerateHoaDon(List<CaChieu> caChieuList, List<NguoiDung> nguoiDungList, AppDbContext context)
  {
    var random = new Random();
    List<LoaiGhe> loaiGheList =
    [..
      from g in context.LoaiGhe
      orderby g.MaLoaiGhe
      select g
    ];

    uint MaHoaDon = 0;
    foreach (var caChieu in caChieuList)
    {
      List<Ghe> gheList = [];
      foreach (var nguoiDung in nguoiDungList)
      {
        HoaDon hoaDon = new()
        {
          MaHoaDon = MaHoaDon++,
          MaNguoiDung = nguoiDung.MaNguoiDung,
          ThoiGianDat = DateTime.UtcNow.Date,
        };
        context.HoaDon.Add(hoaDon);
        context.SaveChanges();

        uint hang, cot;
        do
        {
          hang = (uint)random.Next((int)caChieu.SoCot);
          cot = (uint)random.Next((int)caChieu.SoCot);
        } while (gheList.Where(i => i.Cot == hang && i.Hang == cot).Any());

        Ghe ghe = new()
        {
          MaLoaiGhe = hang is >= 3 and <= 5 ? loaiGheList[1].MaLoaiGhe : loaiGheList[0].MaLoaiGhe,
          MaCaChieu = caChieu.MaCaChieu,
          MaHoaDon = hoaDon.MaHoaDon,
          Cot = cot,
          Hang = hang
        };
        gheList.Add(ghe);

      }

      context.Ghe.AddRange(gheList);
      context.SaveChanges();
    }

    return [];
  }
}