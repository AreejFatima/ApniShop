using ApniShop_A3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop_A3.ViewModels
{
    public class ShoppingViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        virtual public string Category { get; set; }
    }
}
