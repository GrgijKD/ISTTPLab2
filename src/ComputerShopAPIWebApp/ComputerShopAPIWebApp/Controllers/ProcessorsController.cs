using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerShopAPIWebApp.Models;

namespace ComputerShopAPIWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessorsController : ControllerBase
    {
        private readonly ShopAPIContext _context;

        public ProcessorsController(ShopAPIContext context)
        {
            _context = context;
        }

        // GET: api/Processors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Processor>>> GetProcessors()
        {
            return await _context.Processors.ToListAsync();
        }

        // GET: api/Processors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Processor>> GetProcessor(int id)
        {
            var processor = await _context.Processors.FindAsync(id);

            if (processor == null)
                return NotFound();

            return processor;
        }

        // POST: api/Processors
        [HttpPost]
        public async Task<ActionResult<Processor>> PostProcessor(Processor processor)
        {
            _context.Processors.Add(processor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProcessor), new { id = processor.Id }, processor);
        }

        // PUT: api/Processors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcessor(int id, Processor processor)
        {
            if (id != processor.Id)
                return BadRequest();

            _context.Entry(processor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessorExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Processors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcessor(int id)
        {
            var processor = await _context.Processors.FindAsync(id);
            if (processor == null)
                return NotFound();

            _context.Processors.Remove(processor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcessorExists(int id)
        {
            return _context.Processors.Any(e => e.Id == id);
        }
    }
}
