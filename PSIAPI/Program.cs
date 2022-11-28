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

/*var configuration = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<LocationItem, LocationItem>();
});
var mapper = new Mapper(configuration);*/
/*builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());*/


/*app.UseHttpsRedirection();

app.UseAuthorization();*/


var app = builder.Build();
app.MapControllers();


app.MapGet("api/psi", async (AppDbContext context) =>
{
    var items = await context.LocationItems.ToListAsync();

    return Results.Ok(items);
});


app.MapPost("api/psi", async (AppDbContext context, LocationItem locationItem) =>
{
    await context.LocationItems.AddAsync(locationItem);

    await context.SaveChangesAsync();

    return Results.Created($"api/psi/{locationItem.Id}", locationItem);
});

app.MapPut("api/psi/{id}", async (AppDbContext context, string id, LocationItem locationItem) =>
{
    var locationItemModel = await context.LocationItems.FirstOrDefaultAsync(t => t.Id.Equals(id));

    if (locationItemModel == null)
    {
        return Results.NotFound();
    }

    locationItemModel.State = locationItem.State;
    locationItemModel.City = locationItem.City;
    locationItemModel.Street = locationItem.Street;
    locationItemModel.Longitude = locationItem.Longitude;
    locationItemModel.Latitude = locationItem.Latitude;

    await context.SaveChangesAsync();

    return Results.NoContent();
});


app.MapDelete("api/psi/{id}", async (AppDbContext context, string id) =>
{
    var locationItemModel = await context.LocationItems.FirstOrDefaultAsync(t => t.Id.Equals(id));

    if (locationItemModel == null)
    {
        return Results.NotFound();
    }

    context.LocationItems.Remove(locationItemModel);

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();