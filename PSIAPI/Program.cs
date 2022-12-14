using Microsoft.EntityFrameworkCore;
using PSIAPI.Data;
using PSIAPI.Interfaces;
using PSIAPI.Services;


var builder = WebApplication.CreateBuilder(args);


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

app.UseLogRequests();

app.Run();