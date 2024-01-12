using System.ComponentModel.DataAnnotations;

namespace Services.Services.BasketService.Dto
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Price Must be Greater than Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,10, ErrorMessage = "Quantity must be between 1 and 10 pieces")]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}