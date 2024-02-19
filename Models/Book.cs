using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BjornsBookShop2024.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int? Price { get; set; }
        public string? PicturePath { get; set; }
    }
}
