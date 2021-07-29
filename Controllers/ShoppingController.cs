using ApniShop_A3.Models;
using ApniShop_A3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop_A3.Controllers
{
    public class ShoppingController : Controller
    {
        private OurDbContext db;
        
        public ShoppingController()
        {
            db = new OurDbContext();
        }
        public IActionResult DisplayProducts()
        {
             IEnumerable<ShoppingViewModel> listOfShoppingViewModels = (from objItem in db.Items
                    join
                        objCate in db.Categories
                        on objItem.CategoryID equals objCate.CategoryID
                    select new ShoppingViewModel()
                    {
                        ImgUrl = objItem.ImgUrl,
                        ItemName = objItem.ItemName,
                        Description = objItem.Description,
                        Price = objItem.Price,
                        ItemId = objItem.ItemID,
                        Category = objCate.CategoryName,
                        
                    }

                ).ToList();
            return View(listOfShoppingViewModels);
        }
    }
}
