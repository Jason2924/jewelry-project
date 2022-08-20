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
    public class CertificateController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/Certificate
        public ActionResult Index()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            return View(db.CertificateMsts.ToList());
        }

        // GET: Admin/Certificate/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificateMst certificateMst = db.CertificateMsts.Find(id);
            if (certificateMst == null)
            {
                return HttpNotFound();
            }
            return View(certificateMst);
        }

        // GET: Admin/Certificate/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        // POST: Admin/Certificate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type")] CertificateMst certificateMst)
        {
            if (ModelState.IsValid)
            {
                db.CertificateMsts.Add(certificateMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(certificateMst);
        }

        // GET: Admin/Certificate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificateMst certificateMst = db.CertificateMsts.Find(id);
            if (certificateMst == null)
            {
                return HttpNotFound();
            }
            return View(certificateMst);
        }

        // POST: Admin/Certificate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type")] CertificateMst certificateMst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(certificateMst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(certificateMst);
        }

        // GET: Admin/Certificate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificateMst certificateMst = db.CertificateMsts.Find(id);
            if (certificateMst == null)
            {
                return HttpNotFound();
            }
            return View(certificateMst);
        }

        // POST: Admin/Certificate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CertificateMst certificateMst = db.CertificateMsts.Find(id);
            db.CertificateMsts.Remove(certificateMst);
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
            IEnumerable<CertificateMst> certificateList = new List<CertificateMst>();
            certificateList = db.CertificateMsts.Where(x => x.Type.Contains(key));
            return View(certificateList);
        }
    }
}