namespace ComputerShopAPIWebApp.DTOs
{
    public class ProcessorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Cores { get; set; } = null!;
        public string Frequency { get; set; } = null!;
    }
}