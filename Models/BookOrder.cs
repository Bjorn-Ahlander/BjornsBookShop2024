using System.ComponentModel.DataAnnotations;

namespace BjornsBookShop2024.Models
{
    public class BookOrder
    {
        [Key]
        public int BookOrderId { get; set; }
        public int BookId { get; set; } // Foreign Key
        public int OrderId { get; set; }// Foreign Key
        public int Quantity { get; set; }

        public virtual Book Book { get; set; } // Navigation property för att navigera till boken
        public virtual Order Order { get; set; } // Navigation property för att navigera till ordern
    }
}
