using System.ComponentModel.DataAnnotations;

namespace ComputerShopAPIWebApp.Models
{
    public class GPU
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Об'єм відеопам'яті")]
        public required string VRAM { get; set; }

        public ICollection<Computer> Computers { get; set; } = new List<Computer>();
    }
}
