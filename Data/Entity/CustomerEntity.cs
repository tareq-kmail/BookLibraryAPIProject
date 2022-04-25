using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
   public class CustomerEntity
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public string NationalId { set; get; }

        [Required]
        public string Name { set; get; }
        public string Phone { set; get; }
        public string City { set; get; }
    }
}
