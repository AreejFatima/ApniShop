using ApniShop_A3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApniShop_A3.Controllers
{
    public class StoreController : Controller
    {
        OurDbContext ApniShopDb = new OurDbContext();
        public ActionResult Index()
        {
            var category = ApniShopDb.Categories.ToList();
            return View(category);
        }
        public ActionResult Browse(string category)
        {
            var Cat = ApniShopDb.Categories.Include("Items").Single(c=>c.CategoryName==category);
            return View(Cat);
        }
        public ActionResult Details(int id)
        {
            var item = ApniShopDb.Items.Find(id);
            return View(item);
        }
        public IActionResult Buy(int id)
        {
            var item = ApniShopDb.Items.Find(id);
            var Order = new Order
            {
                ItemID = id,
                Item = item,
                Total = item.Price,
                UserID= int.Parse(HttpContext.Session.GetString("UserID"))
            };
            return View(Order);
        }
        [HttpPost]
        public IActionResult OrderDetails([Bind(include: "OrderId,UserID,ItemID,Total,Address,City,Country,State")] Order order)
        {
            if (ModelState.IsValid)
            {
                ApniShopDb.Orders.Add(order);
                ApniShopDb.SaveChanges();
            }
            return View(order);
        }
        public ActionResult ShowSearchForm()
        {
            return View();
        }
        public ActionResult ShowSearchResults(String SearchPhrase)
        {
            return View("AllItems", ApniShopDb.Items.Where(i => i.ItemName.Contains(SearchPhrase)).ToList());
        }
        public ActionResult AllItems()
        {
            var items = ApniShopDb.Items.ToList();
            return View(items);
        }

    }
}
