using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop_A3.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        public int ItemID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public decimal Total { get; set; }
        virtual public Item Item { get; set; }
        virtual public User User { get; set; }
    }
}
