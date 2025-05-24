using System.ComponentModel.DataAnnotations;

namespace ComputerShopAPIWebApp.Models
{
    public class RAM
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Об'єм")]
        public required string Amount { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Тип")]
        public required string Type { get; set; }

        public ICollection<Computer> Computers { get; set; } = new List<Computer>();
    }
}
