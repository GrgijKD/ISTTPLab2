using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerShopAPIWebApp.Models;

namespace ComputerShopAPIWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GPUsController : ControllerBase
    {
        private readonly ShopAPIContext _context;

        public GPUsController(ShopAPIContext context)
        {
            _context = context;
        }

        // GET: api/GPUs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GPU>>> GetGPUs()
        {
            return await _context.GPUs.ToListAsync();
        }

        // GET: api/GPUs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GPU>> GetGPU(int id)
        {
            var gpu = await _context.GPUs.FindAsync(id);

            if (gpu == null)
                return NotFound();

            return gpu;
        }

        // POST: api/GPUs
        [HttpPost]
        public async Task<ActionResult<GPU>> PostGPU(GPU gpu)
        {
            _context.GPUs.Add(gpu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGPU), new { id = gpu.Id }, gpu);
        }

        // PUT: api/GPUs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGPU(int id, GPU gpu)
        {
            if (id != gpu.Id)
                return BadRequest();

            _context.Entry(gpu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GPUExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/GPUs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGPU(int id)
        {
            var gpu = await _context.GPUs.FindAsync(id);
            if (gpu == null)
                return NotFound();

            _context.GPUs.Remove(gpu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GPUExists(int id)
        {
            return _context.GPUs.Any(e => e.Id == id);
        }
    }
}
