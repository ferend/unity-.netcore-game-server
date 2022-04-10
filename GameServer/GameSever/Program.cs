using GameSever.Services;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// This will help us to return properties as pascal case.
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

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
