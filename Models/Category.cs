using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop_A3.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Decription { get; set; }
        public List<Item> Items { get; set; }
    }
}
