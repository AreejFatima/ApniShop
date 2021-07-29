using ApniShop_A3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ApniShop_A3.Controllers
{
  
    public class StoreMgrController : Controller
    {
        OurDbContext Db = new OurDbContext();
        // GET: StoreMgrController
        public ActionResult Index()
        {
            var items = Db.Items.Include(i => i.Category).Include(i => i.Maker);


            return View(items.ToList());
        }

        // GET: StoreMgrController/Details/5
        public ActionResult Details(int id)
        {
            var Item = Db.Items.Find(id);
            return View(Item);
        }

        // GET: StoreMgrController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(Db.Categories, "CategoryID", "CategoryName");
            ViewBag.MakerID = new SelectList(Db.Makers, "MakerID", "MakerName");
            return View();
        }
        

        // POST: StoreMgrController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "ItemID,CategoryID,MakerID,ItemName,Price,ImgUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                Db.Items.Add(item);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(Db.Categories, "CategoryID", "CategoryName",item.CategoryID);
            ViewBag.MakerID = new SelectList(Db.Makers, "MakerID", "MakerName",item.MakerID);
            return View(item);
        }

        // GET: StoreMgrController/Edit/5
        public ActionResult Edit(int id)
        {
            Console.WriteLine(id);
            Item item = Db.Items.Find(id);
            ViewBag.CategoryID = new SelectList(Db.Categories, "CategoryID", "CategoryName", item.CategoryID);
            ViewBag.MakerID = new SelectList(Db.Makers, "MakerID", "MakerName", item.MakerID);
            return View(item);
        }

        // POST: StoreMgrController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "ItemID,CategoryID,MakerID,ItemName,Price,ImgUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(item).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(Db.Categories, "CategoryID", "CategoryName", item.CategoryID);
            ViewBag.MakerID = new SelectList(Db.Makers, "MakerID", "MakerName", item.MakerID);
            return View(item);
        }

        // GET: StoreMgrController/Delete/5
        public ActionResult Delete(int id)
        {
            Item item = Db.Items.Find(id);
            return View();
        }

        // POST: StoreMgrController/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            Item item = Db.Items.Find(id);
            Db.Items.Remove(item);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
