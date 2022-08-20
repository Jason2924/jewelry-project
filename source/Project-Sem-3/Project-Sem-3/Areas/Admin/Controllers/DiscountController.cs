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
    public class DiscountController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/Discount
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            var discounts = db.DiscountMsts.Include(d => d.ItemMst);
            return View(discounts.ToList());
        }

        // GET: Admin/Discount/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountMst discount = db.DiscountMsts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // GET: Admin/Discount/Create
        public ActionResult Create(string styleCode)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (styleCode != null)
            {
                ViewBag.Item_ID = new SelectList(db.ItemMsts.Where(i => i.StyleCode == styleCode).ToList(), "ID", "StyleCode");
            }
            else
            {
                ViewBag.Item_ID = new SelectList(db.ItemMsts, "ID", "StyleCode");
            }
            return View();
        }

        // POST: Admin/Discount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Item_ID,Percentage,Description,StartDate,EndDate")] DiscountMst discount)
        {
            if (ModelState.IsValid)
            {
                db.DiscountMsts.Add(discount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Item_ID = new SelectList(db.ItemMsts, "ID", "StyleCode", discount.Item_ID);
            return View(discount);
        }

        // GET: Admin/Discount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountMst discount = db.DiscountMsts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            ViewBag.Item_ID = new SelectList(db.ItemMsts, "ID", "StyleCode", discount.Item_ID);
            return View(discount);
        }

        // POST: Admin/Discount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Item_ID,Percentage,Description,StartDate,EndDate")] DiscountMst discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Item_ID = new SelectList(db.ItemMsts, "ID", "StyleCode", discount.Item_ID);
            return View(discount);
        }

        // GET: Admin/Discount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountMst discount = db.DiscountMsts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Admin/Discount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiscountMst discount = db.DiscountMsts.Find(id);
            db.DiscountMsts.Remove(discount);
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
            IEnumerable<DiscountMst> discountList = new List<DiscountMst>();
            discountList = db.DiscountMsts.Where(x => x.ItemMst.StyleCode.Contains(key));
            return View(discountList);
        }
    }
}