using Microsoft.AspNetCore.Mvc;
using PSIAPI.Interfaces;
using PSIAPI.Models;

namespace TodoAPI.Controllers
{

    [ApiController]
    [Route($"api/{_endpointName}")]
    public class LogItemController : ControllerBase
    {
        private const string _endpointName = "log";
        private readonly ILogItemRepository _repo;

        public LogItemController(ILogItemRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LogItem item)
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
            return Created($"api/{_endpointName}/{item.ID}", item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
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
