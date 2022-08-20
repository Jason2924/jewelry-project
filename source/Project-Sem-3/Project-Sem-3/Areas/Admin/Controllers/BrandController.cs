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
    public class BrandController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/Brand
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            return View(db.BrandMsts.ToList());
        }

        // GET: Admin/Brand/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandMst brandMst = db.BrandMsts.Find(id);
            if (brandMst == null)
            {
                return HttpNotFound();
            }
            return View(brandMst);
        }

        // GET: Admin/Brand/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        // POST: Admin/Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] BrandMst brandMst)
        {
            if (ModelState.IsValid)
            {
                db.BrandMsts.Add(brandMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brandMst);
        }

        // GET: Admin/Brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandMst brandMst = db.BrandMsts.Find(id);
            if (brandMst == null)
            {
                return HttpNotFound();
            }
            return View(brandMst);
        }

        // POST: Admin/Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] BrandMst brandMst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brandMst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brandMst);
        }

        // GET: Admin/Brand/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrandMst brandMst = db.BrandMsts.Find(id);
            if (brandMst == null)
            {
                return HttpNotFound();
            }
            return View(brandMst);
        }

        // POST: Admin/Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BrandMst brandMst = db.BrandMsts.Find(id);
            db.BrandMsts.Remove(brandMst);
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
            IEnumerable<BrandMst> brandList = new List<BrandMst>();
            brandList = db.BrandMsts.Where(x => x.Name.Contains(key));
            return View(brandList);
        }
    }
}