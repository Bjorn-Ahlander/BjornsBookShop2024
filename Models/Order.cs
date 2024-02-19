using System.ComponentModel.DataAnnotations;

namespace BjornsBookShop2024.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public int? TotalSum { get; set; }
    }
}
