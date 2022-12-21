using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSIAPI.Interfaces;
using PSIAPI.Models;
using PSIAPI.Interceptors;
using Autofac.Extras.DynamicProxy;

namespace PSIAPI.Controllers
{
    [ApiController]
    [Route($"api/{_endpoint}")]
    public class LocationItemController : ControllerBase
    {
        private const string _endpoint = "location";
        private readonly ILocationItemRepository _repo;

        public LocationItemController(ILocationItemRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            try
            {
                var item = await _repo.GetByIdAsync(id);
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Item with ID doesn't exist");
                }
                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't find item");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LocationItemDto item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid item");
                }
                bool itemExists = await _repo.ExistsAsync(item.ID);
                if (itemExists)
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Item with ID already exists");
                }
                await _repo.AddAsync(item);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't create item");
            }
            return Created($"api/{_endpoint}/{item.ID}", item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody] LocationItemDto item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid item");
                }
                LocationItemDto? existingItem = await _repo.FindAsync(id);
                if (existingItem == null)
                {
                    return NotFound("Item with ID doesn't exist");
                }
                await _repo.UpdateAsync(existingItem, item);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't update item");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var item = await _repo.FindAsync(id);
                if (item == null)
                {
                    return NotFound("Item with ID doesn't exist");
                }
                await _repo.DeleteAsync(item);
            }
            catch (Exception)
            {
                return BadRequest("Couldn't delete item");
            }
            return NoContent();
        }
    }
}
