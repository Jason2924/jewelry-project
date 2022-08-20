using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Controllers
{
    public class AccountController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();
        // GET: Account

        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Profile");
            }
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }

        [HttpPost, ActionName("Login")]
        public ActionResult Login(string username, string password)
        {
            UserMst user = db.UserMsts.SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                password = Utinity.EncodeMD5(password);
                if (user.Password == password)
                {
                    Session["user"] = user.Username;
                    Session["password"] = user.Password;
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Error = "Username or Password incorrect";
            return View();
        }

        public ActionResult Register()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }

        [HttpPost, ActionName("Register")]
        public ActionResult Register(UserMst user, string confirm)
        {
            if (ModelState.IsValid)
            {
                UserMst userExitst = db.UserMsts.SingleOrDefault(u => u.Username == user.Username);
                if (userExitst == null)
                {
                    if (user.Password != null && user.Password != "")
                    {
                        if (user.Password == confirm)
                        {
                            user.Password = Utinity.EncodeMD5(user.Password);
                            db.UserMsts.Add(user);
                            db.SaveChanges();
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            ViewBag.Confirm = "Confirm not match with Password";
                        }
                    } else
                    {
                        ViewBag.Password = "Can not empty";
                    }
                } else
                {
                    ViewBag.Error = "Username has existed";
                }
            }
            return View(user);
        }

        public ActionResult Edit()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            string username = (string)Session["user"];
            UserMst user = db.UserMsts.SingleOrDefault(u => u.Username == username);
            return View(user);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(UserMst user, string confirm)
        {
            if (ModelState.IsValid)
            {
                if (user.Password == null)
                {
                    string password = (string)Session["password"];
                    user.Password = password;
                }
                else
                {
                    if (user.Password == confirm)
                    {
                        user.Password = Utinity.EncodeMD5(user.Password);
                    } else
                    {
                        ViewBag.Confirm = "Confirm not match with Password";
                        return View(user);
                    }
                }
                user.Username = (string)Session["user"];
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Session["user"] = user.Username;
                Session["password"] = user.Password;
                return RedirectToAction("Profile", "Account");
            }
            return View(user);
        }

        public ActionResult Profile()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            string username = (string)Session["user"];
            UserMst user = db.UserMsts.SingleOrDefault(u => u.Username == username);
            List<OrderMst> orderList = db.OrderMsts.Where(o => o.User_ID == user.ID).ToList();
            ViewBag.OrderList = orderList;
            return View(user);
        }

        public ActionResult ForgottenPassword()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }

        [HttpPost, ActionName("ForgottenPassword")]
        public ActionResult ForgottenPassword(string username)
        {
            var user = db.UserMsts.SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                var password = Utinity.RamdomString(10);
                user.Password = Utinity.EncodeMD5(password);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                var body = "This is your new password : " + password + "\nPlease login ang change your new password!";
                Mail mail = new Mail(user.Email, "Yash Gems forgotten password", password);
                mail.BuildMail();
                return RedirectToAction("Login");
            }
            ViewBag.Error = "Username has not existed";
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["password"] = null;
            return RedirectToAction("Login");
        }
    }
}