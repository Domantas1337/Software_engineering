using Microsoft.EntityFrameworkCore;
using PSIAPI.Data;
using PSIAPI.Models;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});


var app = builder.Build();


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

    locationItem.State = locationItem.State;
    locationItemModel.City = locationItem.City;
    locationItemModel.Street = locationItem.Street;
    locationItemModel.Longitude = locationItem.Longitude;
    locationItemModel.Latitude = locationItem.Latitude;

    await context.SaveChangesAsync();

    return Results.NoContent();
});


app.MapDelete("api/psi/{id}", async(AppDbContext context, int id) =>
{
    var locationItemModel = await context.LocationItems.FirstOrDefaultAsync(t => t.Id == id);

    if (locationItemModel == null)
    {
        return Results.NotFound();
    }

    context.LocationItems.Remove(locationItemModel);

    await context.SaveChangesAsync();

    return Results.NoContent();
});
    

app.Run();