using ApniShop_A3.Models;
using ApniShop_A3.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApniShop_A3.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model, User user)
        {
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.Password = user.Password;
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.UserAccount.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            using (OurDbContext db= new OurDbContext())
            {
                var usr = db.UserAccount.Where(u => u.Email == login.Email && u.Password == login.Password).FirstOrDefault();
                if (usr != null)
                {
                    HttpContext.Session.SetString("UserID", usr.UserID.ToString());
                    HttpContext.Session.SetString("UserName", usr.UserName.ToString());
                    return RedirectToAction("LoggedIn");

                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong");
                }
            }
            return View();
        }
        public IActionResult LoggedIn()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return RedirectToAction("Index","Store");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserID");
            return RedirectToAction("Login");
            
        }
        public IActionResult EditProfile()
        {


            using (OurDbContext db = new OurDbContext())
            {

                int userid = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
                if (userid == 0)
                {

                    ModelState.AddModelError("", "User is not logged in");
                    return RedirectToAction("Login");
                }
                return View(db.UserAccount.Find(userid));
            }
        }
        [HttpPost]
        public IActionResult EditProfile(User uNew)
        {

            using (OurDbContext db = new OurDbContext())
            {
                try
                {
                    int userid = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
                    if (userid == 0)
                    {
                        return View();
                    }

                    User dbUser = db.UserAccount.Find(userid);
                    dbUser.UserName = uNew.UserName;
                    dbUser.Password = uNew.Password;
                    dbUser.PhoneNumber = uNew.PhoneNumber;
                    dbUser.Location = uNew.Location;
                    dbUser.Email = uNew.Email;
                    dbUser.ConfirmPassword = uNew.ConfirmPassword;
                    db.Entry(dbUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return View();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }
    }
}
