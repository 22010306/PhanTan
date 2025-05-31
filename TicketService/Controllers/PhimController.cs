
using DatabaseModels;
using DatabaseModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TicketService.Controllers;

[ApiController]
[Route("[controller]")]
public class PhimController(AppDbContext context) : ControllerBase
{
  readonly AppDbContext dbContext = context;

  [HttpGet("danh-sach-phim")]
  public async Task<ActionResult> Get()
  {
    var result =
      from p in dbContext.Phim
      select new
      {
        p.MaPhim,
        p.TenPhim,
        p.DoDai,
        p.DanhGia,
        theLoai = (
        from ptl in dbContext.Phim_TheLoai
        join tl in dbContext.TheLoai on ptl.MaTheLoai equals tl.MaTheLoai
        where ptl.MaPhim == p.MaPhim
        select new { tl.MaTheLoai, tl.TenTheLoai }).ToList(),
        caChieu = (
          from ch in dbContext.CaChieu
          join lh in dbContext.LoaiHinh on ch.MaLoaiHinh equals lh.MaLoaiHinh
          where ch.MaPhim == p.MaPhim
          select new
          {
            ch.MaCaChieu,
            ch.ThoiGianChieu,
            ch.SoCot,
            ch.SoHang,
            lh.MaLoaiHinh,
            lh.TenLoaiHinh,
            ghe = (
              from g in dbContext.Ghe
              where g.MaCaChieu == ch.MaCaChieu
              select new
              {
                g.Hang,
                g.Cot,
                g.MaLoaiGhe
              }).ToList()
          }).ToList()
      };

    return Ok(await result.ToListAsync());
  }

  [HttpGet("loai-ghe")]
  public ActionResult GetLoaiGhe()
  {
    return Ok((
      from l in dbContext.LoaiGhe
      select new
      {
        l.GiaTien,
        l.TenLoaiGhe,
        l.MaLoaiGhe
      }).ToList());
  }

  [HttpPost("dat-ve")]
  [Authorize]
  public ActionResult DatVe(DatVeInput input)
  {
    uint userId = uint.Parse(User.FindFirst("Id")!.Value);
    NguoiDung? user = dbContext.NguoiDung.Where(u => u.MaNguoiDung == userId).FirstOrDefault();

    if (user is null) return NotFound();

    uint MaHoaDon = (uint)(dbContext.HoaDon.Count() + 1);

    HoaDon hoaDon = new()
    {
      MaHoaDon = MaHoaDon,
      MaNguoiDung = user.MaNguoiDung,
      ThoiGianDat = DateTime.UtcNow.Date,
    };
    dbContext.HoaDon.Add(hoaDon);

    List<Ghe> ghe = [];
    foreach (var item in input.Ghe)
    {
      Ghe gh = new()
      {
        Hang = item.Hang,
        Cot = item.Cot,
        MaLoaiGhe = item.MaLoaiGhe,
        MaCaChieu = input.MaCaChieu,
        MaHoaDon = hoaDon.MaHoaDon,
      };
      ghe.Add(gh);
    }
    dbContext.Ghe.AddRange(ghe);
    dbContext.SaveChanges();

    return Ok();
  }

  [HttpGet("ca-chieu")]
  public async Task<ActionResult> GetCaChieu(uint id)
  {
    return Ok((
      from c in dbContext.CaChieu
      join g in dbContext.Ghe on c.MaCaChieu equals g.MaCaChieu
      where c.MaCaChieu == id
      select new
      {
        g.Cot,
        g.Hang,
        g.MaLoaiGhe
      }).ToList());
  }
}

public class DatVeInput
{
  public required uint MaCaChieu { get; set; }

  public required List<GheInput> Ghe { get; set; } = null!;
}

public class GheInput
{
  public uint Hang { get; set; }
  public uint Cot { get; set; }
  public uint MaLoaiGhe { get; set; }

}
