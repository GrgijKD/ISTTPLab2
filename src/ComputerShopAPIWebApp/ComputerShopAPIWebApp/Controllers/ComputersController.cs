using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerShopAPIWebApp.Models;
using ComputerShopAPIWebApp.DTOs;

namespace ComputerShopAPIWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputersController : ControllerBase
    {
        private readonly ShopAPIContext _context;

        public ComputersController(ShopAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerDTO>>> GetComputers(string? type)
        {
            var query = _context.Computers
                .Include(c => c.RAM)
                .Include(c => c.Processor)
                .Include(c => c.GPU)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(c => c.Type.ToLower() == type.ToLower());

            var computers = await query
                .Select(c => new ComputerDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    Brand = c.Brand,
                    ScreenSize = c.ScreenSize,
                    Resolution = c.Resolution,
                    Storage = c.Storage,
                    Price = c.Price,
                    RAM = new RAMDto { Id = c.RAM.Id, Name = c.RAM.Name },
                    Processor = new ProcessorDto { Id = c.Processor.Id, Name = c.Processor.Name },
                    GPU = c.GPU != null ? new GpuDto { Id = c.GPU.Id, Name = c.GPU.Name } : null
                })
                .ToListAsync();

            return Ok(computers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerDTO>> GetComputer(int id)
        {
            var computer = await _context.Computers
                .Include(c => c.RAM)
                .Include(c => c.Processor)
                .Include(c => c.GPU)
                .Where(c => c.Id == id)
                .Select(c => new ComputerDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    Brand = c.Brand,
                    ScreenSize = c.ScreenSize,
                    Resolution = c.Resolution,
                    Storage = c.Storage,
                    Price = c.Price,
                    RAM = new RAMDto { Id = c.RAM.Id, Name = c.RAM.Name },
                    Processor = new ProcessorDto { Id = c.Processor.Id, Name = c.Processor.Name },
                    GPU = c.GPU != null ? new GpuDto { Id = c.GPU.Id, Name = c.GPU.Name } : null
                })
                .FirstOrDefaultAsync();

            if (computer == null)
                return NotFound();

            return Ok(computer);
        }

        [HttpPost]
        public async Task<IActionResult> PostComputer([FromBody] ComputerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Price <= 0 || dto.Price % 1 != 0)
                return BadRequest(new { error = "Ціна має бути цілим числом більше 0." });

            if (!await _context.RAMs.AnyAsync(r => r.Id == dto.RAMId))
                return BadRequest(new { error = "Оперативна пам'ять не знайдена." });

            if (!await _context.Processors.AnyAsync(p => p.Id == dto.ProcessorId))
                return BadRequest(new { error = "Процесор не знайдений." });

            if (dto.GPUID != null && !await _context.GPUs.AnyAsync(g => g.Id == dto.GPUID))
                return BadRequest(new { error = "Відеокарта не знайдена." });

            var computer = new Computer
            {
                Name = dto.Name,
                Type = dto.Type,
                Brand = dto.Brand,
                ScreenSize = dto.ScreenSize,
                Resolution = dto.Resolution,
                Storage = dto.Storage,
                RAMId = dto.RAMId,
                ProcessorId = dto.ProcessorId,
                GPUID = dto.GPUID,
                Price = dto.Price
            };

            _context.Computers.Add(computer);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, computer.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputer(int id, [FromBody] ComputerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Price <= 0 || dto.Price % 1 != 0)
                return BadRequest(new { error = "Ціна має бути цілим числом більше 0." });

            if (!await _context.RAMs.AnyAsync(r => r.Id == dto.RAMId))
                return BadRequest(new { error = "Оперативна пам'ять не знайдена." });

            if (!await _context.Processors.AnyAsync(p => p.Id == dto.ProcessorId))
                return BadRequest(new { error = "Процесор не знайдений." });

            if (dto.GPUID != null && !await _context.GPUs.AnyAsync(g => g.Id == dto.GPUID))
                return BadRequest(new { error = "Відеокарта не знайдена." });

            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
                return NotFound();

            computer.Name = dto.Name;
            computer.Type = dto.Type;
            computer.Brand = dto.Brand;
            computer.ScreenSize = dto.ScreenSize;
            computer.Resolution = dto.Resolution;
            computer.Storage = dto.Storage;
            computer.RAMId = dto.RAMId;
            computer.ProcessorId = dto.ProcessorId;
            computer.GPUID = dto.GPUID;
            computer.Price = dto.Price;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputer(int id)
        {
            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
                return NotFound();

            _context.Computers.Remove(computer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComputerExists(int id)
        {
            return _context.Computers.Any(e => e.Id == id);
        }
    }
}