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
    public class CategoryController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/Category
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            return View(db.CategoryMsts.ToList());
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryMst categoryMst = db.CategoryMsts.Find(id);
            if (categoryMst == null)
            {
                return HttpNotFound();
            }
            return View(categoryMst);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] CategoryMst categoryMst)
        {
            if (ModelState.IsValid)
            {
                db.CategoryMsts.Add(categoryMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoryMst);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryMst categoryMst = db.CategoryMsts.Find(id);
            if (categoryMst == null)
            {
                return HttpNotFound();
            }
            return View(categoryMst);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] CategoryMst categoryMst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryMst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryMst);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryMst categoryMst = db.CategoryMsts.Find(id);
            if (categoryMst == null)
            {
                return HttpNotFound();
            }
            return View(categoryMst);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryMst categoryMst = db.CategoryMsts.Find(id);
            db.CategoryMsts.Remove(categoryMst);
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
            IEnumerable<CategoryMst> categoryList = new List<CategoryMst>();
            categoryList = db.CategoryMsts.Where(x => x.Name.Contains(key));
            return View(categoryList);
        }
    }
}