namespace ComputerShopAPIWebApp.DTOs
{
    public class ComputerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string ScreenSize { get; set; } = null!;
        public string Resolution { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public decimal Price { get; set; }

        public RAMDto RAM { get; set; } = null!;
        public ProcessorDto Processor { get; set; } = null!;
        public GpuDto? GPU { get; set; }
    }
}