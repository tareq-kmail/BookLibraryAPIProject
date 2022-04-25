using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
   public class RentalModel
    {
        public int Id { set; get; }
        public int CustomerId { set; get; }
        public int BookId { set; get; }
        public double Price { set; get; }
        public string BookingDate { set; get; }
        public string BookingExpiryDate { set; get; }
        public CustomerModel Customer { set; get; }
        public BookModel Book { set; get; }
    }
}
