using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSIAPI.Data;
using PSIAPI.Interfaces;
using PSIAPI.Models;

namespace PSIAPI.Controllers
{

    [ApiController]
    [Route($"api/{_endpoint}")]
    public class ReportItemController : ControllerBase
    {
        private const string _endpoint = "report";
        private readonly IReportItemRepository _repo;

        public ReportItemController(IReportItemRepository repo)
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
        public async Task<IActionResult> PostAsync([FromBody] ReportItemDto item)
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