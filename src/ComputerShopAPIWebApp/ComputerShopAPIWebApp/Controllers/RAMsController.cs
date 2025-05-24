using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerShopAPIWebApp.Models;

namespace ComputerShopAPIWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RAMsController : ControllerBase
    {
        private readonly ShopAPIContext _context;

        public RAMsController(ShopAPIContext context)
        {
            _context = context;
        }

        // GET: api/RAMs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RAM>>> GetRAMs()
        {
            return await _context.RAMs.ToListAsync();
        }

        // GET: api/RAMs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RAM>> GetRAM(int id)
        {
            var ram = await _context.RAMs.FindAsync(id);

            if (ram == null)
                return NotFound();

            return ram;
        }

        // POST: api/RAMs
        [HttpPost]
        public async Task<ActionResult<RAM>> PostRAM(RAM ram)
        {
            _context.RAMs.Add(ram);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRAM), new { id = ram.Id }, ram);
        }

        // PUT: api/RAMs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRAM(int id, RAM ram)
        {
            if (id != ram.Id)
                return BadRequest();

            _context.Entry(ram).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RAMExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/RAMs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRAM(int id)
        {
            var ram = await _context.RAMs.FindAsync(id);
            if (ram == null)
                return NotFound();

            _context.RAMs.Remove(ram);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RAMExists(int id)
        {
            return _context.RAMs.Any(e => e.Id == id);
        }
    }
}
