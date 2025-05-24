namespace ComputerShopAPIWebApp.DTOs
{
    public class GpuDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string VRAM { get; set; } = null!;
    }
}