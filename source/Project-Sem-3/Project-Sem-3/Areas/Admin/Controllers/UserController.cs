using Project_Sem_3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project_Sem_3.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/User
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            return View(db.UserMsts.ToList());
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMst userMst = db.UserMsts.Find(id);
            if (userMst == null)
            {
                return HttpNotFound();
            }
            return View(userMst);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,FullName,Address,City,Country,Phone,Email,Birthday,CreatedDate")] UserMst userMst)
        {
            if (ModelState.IsValid)
            {
                db.UserMsts.Add(userMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userMst);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMst userMst = db.UserMsts.Find(id);
            if (userMst == null)
            {
                return HttpNotFound();
            }
            return View(userMst);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,FullName,Address,City,Country,Phone,Email,Birthday,CreatedDate")] UserMst userMst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userMst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userMst);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMst userMst = db.UserMsts.Find(id);
            if (userMst == null)
            {
                return HttpNotFound();
            }
            return View(userMst);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserMst userMst = db.UserMsts.Find(id);
            db.UserMsts.Remove(userMst);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(string key)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            IEnumerable<UserMst> userList = new List<UserMst>();
            userList = db.UserMsts.Where(x => (x.Username.Contains(key) || x.FullName.Contains(key)));
            return View(userList);
        }
    }
}