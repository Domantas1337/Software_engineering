using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSIAPI.Data;
using PSIAPI.Models;
using SQLitePCL;

namespace PSIAPI.Controllers
{

    [ApiController]
    [Route($"api/{_endpointName}")]
    public class ReportItemController : ControllerBase
    {
        private const string _endpointName = "report";
        private readonly AppDbContext _context;

        public ReportItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var items = await _context.ReportItems.ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(ReportItem reportItem)
        {
            await _context.ReportItems.AddAsync(reportItem);

            await _context.SaveChangesAsync();

            return Created($"api/{_endpointName}/{reportItem.ID}", reportItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var reportItemModel = await _context.ReportItems.FirstOrDefaultAsync(t => t.ID.Equals(id));

            if (reportItemModel == null)
            {
                return NotFound();
            }

            _context.ReportItems.Remove(reportItemModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}