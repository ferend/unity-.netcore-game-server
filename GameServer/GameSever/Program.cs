using System.Text;
using GameSever;
using GameSever.Models;
using GameSever.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var settings = new Settings();

// Bind our settings to server settings. That will do some magic in background to automatically go through our configuration and bind models.
builder.Configuration.Bind("Settings " , settings);
builder.Services.AddSingleton(settings);

// Add services to the container.

builder.Services.AddDbContext<GameDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

//Scoped: every single time controller is accessed it is going to create.
builder.Services.AddScoped<IPlayerService,PlayerServices>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Base issuing and validation.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.BearerKey)),
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline. // adding middlewares
if (app.Environment.IsDevelopment())
{
}




app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
