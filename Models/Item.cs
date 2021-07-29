using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace ApniShop_A3.Models
{
    public class Item
    {
        [ScaffoldColumn(false)]
        public int ItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        [DisplayName("Category")]
        public int CategoryID { get; set; }
        [DisplayName("Maker")]
        public int MakerID { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        virtual public Category Category { get; set; }
        virtual public Maker Maker { get; set; }
    }
}
