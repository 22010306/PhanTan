using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatabaseModels;
using DatabaseModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(AppDbContext context) : ControllerBase
{
  private readonly AppDbContext _context = context;

  [HttpPost("Login")]
  public async Task<ActionResult> LoginAsync(LoginInput user)
  {
    var users = await _context.NguoiDung.Where(i => i.Email == user.Email).ToListAsync();
    var hasher = new PasswordHasher<object>();

    foreach (var item in users)
    {
      var result = hasher.VerifyHashedPassword(null, item.MatKhauBam, user.MatKhau);
      if (result != PasswordVerificationResult.Success) continue;

      var claims = new List<Claim>()
      {
        new (ClaimTypes.Email, item.Email),
        new (ClaimTypes.Name, item.HoTen),
        new ("Id", item.MaNguoiDung.ToString())
      };
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecret".PadRight(256)));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        claims: claims,
        issuer: "Authenticate",
        audience: "Authenticate",
        signingCredentials: creds,
        expires: DateTime.Now.AddDays(10)
      );
      return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
    return Unauthorized();
  }

  [HttpPost("Register")]
  public async Task<ActionResult> Register(UserInput input)
  {
    PasswordHasher<object> hasher = new();
    NguoiDung user = new()
    {
      Email = input.Email,
      HoTen = input.HoTen,
      MatKhauBam = hasher.HashPassword(null, input.MatKhau)
    };
    await _context.AddAsync(user);
    await _context.SaveChangesAsync();

    return Ok(new { user.Email, user.HoTen, user.MaNguoiDung });

  }

  [HttpGet("Logout")]
  [Authorize]
  public ActionResult Logout()
  {
    return Ok("Ok");
  }

  [HttpGet]
  [Authorize]
  public ActionResult Validate()
  {
    return Ok();
  }
}

public class UserInput
{
  public required string HoTen { get; set; }
  public required string Email { get; set; }
  public required string MatKhau { get; set; }
}

public class LoginInput
{
  public required string Email { get; set; }
  public required string MatKhau { get; set; }
}