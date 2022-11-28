using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PSIAPI.Data;
using PSIAPI.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();



builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});



var app = builder.Build();
app.MapControllers();

app.Run();