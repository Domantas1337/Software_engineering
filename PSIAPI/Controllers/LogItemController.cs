using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSIAPI.Data;
using PSIAPI.Models;
using SQLitePCL;

namespace PSIAPI.Controllers
{

    [ApiController]
    [Route($"api/{_endpointName}")]
    public class LogItemController : ControllerBase
    {
        private const string _endpointName = "log";
        private readonly AppDbContext _context;

        public LogItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var items = await _context.LogItems.ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(LogItem logItem)
        {
            await _context.LogItems.AddAsync(logItem);

            await _context.SaveChangesAsync();

            return Created($"api/{_endpointName}/{logItem.Id}", logItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var logItemModel = await _context.LogItems.FirstOrDefaultAsync(t => t.Id.Equals(id));

            if (logItemModel == null)
            {
                return NotFound();
            }

            _context.LogItems.Remove(logItemModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}