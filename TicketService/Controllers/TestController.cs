using DatabaseModels;
using DatabaseModels.Models;
using DatabaseModels.Samples;
using Microsoft.AspNetCore.Mvc;

namespace TicketService.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController(AppDbContext context) : ControllerBase
{
  readonly AppDbContext context = context;

  [HttpPost]
  public ActionResult Get()
  {

    context.Phim.AddRange(PhimSample.Data);
    context.TheLoai.AddRange(TheLoaiSample.Data);
    context.LoaiHinh.AddRange(LoaiHinhSample.Data);
    context.LoaiGhe.AddRange(LoaiGheSample.Data);
    context.NguoiDung.AddRange(NguoiDungSample.Data);
    context.SaveChanges();

    var phimList = context.Phim.ToList();
    var theLoaiList = context.TheLoai.ToList();
    var loaiHinhList = context.LoaiHinh.ToList();

    foreach (var phim in phimList)
    {
      context.Phim_TheLoai.AddRange(OthersSample.GeneratePhim_TheLoai(phim, theLoaiList));
      context.CaChieu.AddRange(OthersSample.GenerateCaChieu(phim, loaiHinhList));
    }
    context.SaveChanges();

    List<CaChieu> caChieuList = [.. context.CaChieu];
    List<NguoiDung> nguoiDungList = [.. context.NguoiDung];
    // OthersSample.GenerateHoaDon(caChieuList, nguoiDungList, context);

    return Ok();
  }

  [HttpDelete]
  public ActionResult Delete()
  {
    context.Phim_TheLoai.RemoveRange(context.Phim_TheLoai.ToList());
    context.TheLoai.RemoveRange(context.TheLoai.ToList());
    context.LoaiHinh.RemoveRange(context.LoaiHinh.ToList());
    context.LoaiGhe.RemoveRange(context.LoaiGhe.ToList());
    context.Ghe.RemoveRange(context.Ghe.ToList());
    context.HoaDon.RemoveRange(context.HoaDon.ToList());
    context.Phim.RemoveRange(context.Phim.ToList());
    context.CaChieu.RemoveRange(context.CaChieu.ToList());
    context.NguoiDung.RemoveRange(context.NguoiDung.ToList());
    context.SaveChanges();

    return Ok();
  }
}