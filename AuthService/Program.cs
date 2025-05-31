using System.Text;
using DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options =>
    {
        options.SetIsOriginAllowed(_ => true);
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowCredentials();
    });
});
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgresConnection"),
        t => t.MigrationsAssembly("DatabaseModels"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new()
    {
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
          },
          Array.Empty<string>()
        }
      });
});

builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidIssuer = "Authenticate",
        ValidAudience = "Authenticate",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecret".PadRight(256))),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
