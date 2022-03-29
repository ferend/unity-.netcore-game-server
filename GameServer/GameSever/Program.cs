using GameSever.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

//Scoped: every single time controller is accessed it is goin to craete
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
