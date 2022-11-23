using Microsoft.EntityFrameworkCore;
using PSIAPI.Data;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});



var app = builder.Build();
app.MapControllers();

app.Run();