using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PSIAPI.Data;
using PSIAPI.Interfaces;
using PSIAPI.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PASIAPI", Version = "v1" });
});
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});
builder.Services.AddScoped<ILocationItemRepository, LocationItemRepository>();
builder.Services.AddScoped<ILogItemRepository, LogItemRepository>();
builder.Services.AddScoped<IReportItemRepository, ReportItemRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PSIAPI");
    c.RoutePrefix = string.Empty;
});

app.UseLogRequests();

app.Run();