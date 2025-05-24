using System.ComponentModel.DataAnnotations;

namespace ComputerShopAPIWebApp.Models
{
    public class Processor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Кількість ядер")]
        public required string Cores { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Частота")]
        public required string Frequency { get; set; }

        public ICollection<Computer> Computers { get; set; } = new List<Computer>();
    }
}
