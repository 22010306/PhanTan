using DatabaseModels;
using DatabaseModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(AppDbContext context) : ControllerBase
{
  readonly AppDbContext _context = context;

  [HttpGet]
  [Authorize]
  public ActionResult Get()
  {
    uint userId = uint.Parse(User.FindFirst("Id")!.Value);
    NguoiDung? user = _context.NguoiDung.Where(u => u.MaNguoiDung == userId).FirstOrDefault();

    if (user is null) return NotFound();
    return Ok(new { user.MaNguoiDung, user.Email });
  }

  [HttpPut]
  [Authorize]
  public ActionResult Put()
  {
    return Ok("Test");
  }
  [HttpDelete]
  [Authorize]
  public async Task<ActionResult> Delete()
  {
    uint userId = uint.Parse(User.FindFirst("Id")!.Value);
    NguoiDung? user = _context.NguoiDung.Where(u => u.MaNguoiDung == userId).FirstOrDefault();

    if (user is null) return NotFound();
    _context.NguoiDung.Remove(user);
    await _context.SaveChangesAsync();

    return Ok();
  }
}