using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSIAPI.Data;
using PSIAPI.Models;
using SQLitePCL;

namespace TodoAPI.Controllers
{

    [ApiController]
    [Route($"api/{_endpointName}")]
    public class LocationItemsController : ControllerBase
    {
        private const string _endpointName = "location";
        private readonly AppDbContext _context;

        public LocationItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var items = await _context.LocationItems.ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(LocationItem locationItem)
        {
            await _context.LocationItems.AddAsync(locationItem);

            await _context.SaveChangesAsync();

            return Created($"api/{_endpointName}/{locationItem.Id}", locationItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, LocationItem locationItem)
        {
            var locationItemModel = await _context.LocationItems.FirstOrDefaultAsync(t => t.Id.Equals(id));

            if (locationItemModel == null)
            {
                return NotFound();
            }

            locationItemModel.State = locationItem.State;
            locationItemModel.City = locationItem.City;
            locationItemModel.Street = locationItem.Street;
            locationItemModel.Longitude = locationItem.Longitude;
            locationItemModel.Latitude = locationItem.Latitude;
            /*mapper.Map(locationItem, locationItemModel);*/

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var locationItemModel = await _context.LocationItems.FirstOrDefaultAsync(t => t.Id.Equals(id));

            if (locationItemModel == null)
            {
                return NotFound();
            }

            _context.LocationItems.Remove(locationItemModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
