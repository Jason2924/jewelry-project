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
    public class InquiryController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/Inquiry
        public ActionResult Index(bool? sort)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (sort != null)
            {
                return View(db.InquiryMsts.Where(i => i.Status == sort).ToList());
            }
            return View(db.InquiryMsts.ToList());
        }

        // GET: Admin/Inquiry/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InquiryMst inquiryMst = db.InquiryMsts.Find(id);
            if (inquiryMst == null)
            {
                return HttpNotFound();
            }
            return View(inquiryMst);
        }

        // GET: Admin/Inquiry/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        // POST: Admin/Inquiry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FullName,Address,City,Country,Phone,Email,Content,CreatedDate")] InquiryMst inquiryMst)
        {
            if (ModelState.IsValid)
            {
                db.InquiryMsts.Add(inquiryMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inquiryMst);
        }

        // GET: Admin/Inquiry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InquiryMst inquiryMst = db.InquiryMsts.Find(id);
            if (inquiryMst == null)
            {
                return HttpNotFound();
            }
            return View(inquiryMst);
        }

        // POST: Admin/Inquiry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,Address,City,Country,Phone,Email,Content,CreatedDate")] InquiryMst inquiryMst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inquiryMst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inquiryMst);
        }

        // GET: Admin/Inquiry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InquiryMst inquiryMst = db.InquiryMsts.Find(id);
            if (inquiryMst == null)
            {
                return HttpNotFound();
            }
            return View(inquiryMst);
        }

        // POST: Admin/Inquiry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InquiryMst inquiryMst = db.InquiryMsts.Find(id);
            db.InquiryMsts.Remove(inquiryMst);
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
            IEnumerable<InquiryMst> inquiryList = new List<InquiryMst>();
            inquiryList = db.InquiryMsts.Where(x => (x.Content.Contains(key) || x.FullName.Contains(key)));
            return View(inquiryList);
        }

        [HttpPost, ActionName("ChangeStatus")]
        public ActionResult ChangeStatus(int inquiryId, bool status)
        {
            var inquiry = db.InquiryMsts.SingleOrDefault(o => o.ID == inquiryId);
            inquiry.Status = status;
            db.Entry(inquiry).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = inquiryId });
        }
    }
}