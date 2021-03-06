using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class SalesModel
    {
        public int Id { set; get; }
        public int CustomerId { set; get; }
        public int BookId { set; get; }
        public int Quantity { set; get; }
        public double Price { set; get; }
        public string Date { set; get; }
        public CustomerModel Customer { set; get; }
        public BookModel Book  { set; get; }
}
}
