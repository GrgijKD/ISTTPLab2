using System.ComponentModel.DataAnnotations;

namespace ComputerShopAPIWebApp.Models
{
    public class Computer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва")]
        public required string Name { get; set; }

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
        [Display(Name = "Обсяг пам'яті")]
        public required string Storage { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Обсяг оперативної пам'яті")]
        public int RAMId { get; set; }
        public RAM RAM { get; set; } = null!;

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Процесор")]
        public int ProcessorId { get; set; }
        public Processor Processor { get; set; } = null!;

        [Display(Name = "Дискретна відеокарта")]
        public int? GPUID { get; set; }
        public GPU? GPU { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ціна")]
        public required int Price { get; set; }
    }
}
