using Project_Sem_3.Controllers;
using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/Login
        public ActionResult Index()
        {
            // Account for test
            // Username : admin
            // Password : 123
            return View();
        }

        [HttpPost, ActionName("Index")]
        public ActionResult Index(AdminMst account)
        {
            foreach (AdminMst acc in db.AdminMsts.ToList<AdminMst>())
            {
                if (acc.Username == account.Username)
                {
                    string encode = Utinity.EncodeMD5(account.Password);
                    account.Password = encode;
                    if (acc.Password == account.Password)
                    {
                        Session["admin"] = account.Username;
                        return RedirectToAction("Index", "Dashboard");
                    }
                    ModelState.AddModelError("", "Username or Password wrong!");
                    return View();
                }
            }
            ModelState.AddModelError("", "Account has not existed!");
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["admin"] != null)
            {
                Session.Remove("admin");
            }
            return RedirectToAction("Index", "Login");
        }
    }
}