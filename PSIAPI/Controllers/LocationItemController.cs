using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSIAPI.Data;
using PSIAPI.Interfaces;
using PSIAPI.Models;
using SQLitePCL;

namespace TodoAPI.Controllers
{

    [ApiController]
    [Route($"api/{_endpointName}")]
    public class TodoItemsController : ControllerBase
    {
        private const string _endpointName = "location";
        private readonly ILocationRepository LocationRepository;
        private readonly AppDbContext _context;

        public TodoItemsController(ILocationRepository todoRepository, AppDbContext context)
        {
            LocationRepository = todoRepository;
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


        /*        [HttpGet]
                public async Task<IActionResult> ListAsync()
                {
                    *//*            return Ok(LocationRepository.All);*//*
                    return Ok(await _context.LocationItems.ToListAsync());
                }


                [HttpPost]
                public IActionResult Create([FromBody] LocationItem item)
                {
                    try
                    {
                        if (item == null || !ModelState.IsValid)
                        {
                            return BadRequest();
                        }
                        bool itemExists = LocationRepository.DoesItemExist(item.Id);
                        if (itemExists)
                        {
                            return StatusCode(StatusCodes.Status409Conflict);
                        }
                        LocationRepository.Insert(item);
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                    return Ok(item);
                }


                [HttpPut]
                public IActionResult Edit([FromBody] LocationItem item)
                {
                    try
                    {
                        if (item == null || !ModelState.IsValid)
                        {
                            return BadRequest();
                        }
                        var existingItem = LocationRepository.Find(item.Id);
                        if (existingItem == null)
                        {
                            return NotFound();
                        }
                        LocationRepository.Update(item);
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                    return NoContent();
                }


                [HttpDelete("{id}")]
                public IActionResult Delete(string id)
                {
                    try
                    {
                        var item = LocationRepository.Find(id);
                        if (item == null)
                        {
                            return NotFound();
                        }
                        LocationRepository.Delete(id);
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                    return NoContent();
                }*/
    }
}
