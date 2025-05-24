namespace ComputerShopAPIWebApp.DTOs
{
    public class ComputerCreateDto
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string ScreenSize { get; set; }
        public string Resolution { get; set; }
        public string Storage { get; set; } = null!;
        public int RAMId { get; set; }
        public int ProcessorId { get; set; }
        public int? GPUID { get; set; }
        public int Price { get; set; }
    }
}