using System.ComponentModel.DataAnnotations;

namespace ComputerShopAPIWebApp.Models
{
    public class Computer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Тип")]
        public required string Type { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Фірма")]
        public required string Brand { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Діагональ екрану")]
        public required string ScreenSize { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Роздільна здатність")]
        public required string Resolution { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Процесор")]
        public required string Processor { get; set; }

        [Display(Name = "Дискретна відеокарта")]
        public string? GPU { get; set; }

        [Display(Name = "Обсяг відеопам'яті")]
        public string? VRAM { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Обсяг пам'яті")]
        public required string Storage { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Обсяг оперативної пам'яті")]
        public required string RAM { get; set; }
    }
}
