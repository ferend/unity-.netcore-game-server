using GameSever;
using GameSever.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<GameDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

// builder.Services.AddControllers().AddNewtonsoftJson(o =>
// {
//     o.SerializerSettings.ContractResolver = new DefaultContractResolver();
// });

//Scoped: every single time controller is accessed it is going to create.
builder.Services.AddScoped<IPlayerService,PlayerServices>();

var app = builder.Build();

// Configure the HTTP request pipeline. // adding middlewares
if (app.Environment.IsDevelopment())
{
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
