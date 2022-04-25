using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{ 
   public class RentalEntity
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public int CustomerId { set; get; }

        [Required]
        public int BookId { set; get; }

        [Required]
        public double Price { set; get; }
        public string BookingDate { set; get; }

        [Required]
        public string BookingExpiryDate { set; get; }

        [ForeignKey("CustomerId")]
        public CustomerEntity Customer { set; get; }

        [ForeignKey("BookId")]
        public BookEntity Book { set; get; }
    }
}
