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
    public class OrderController : Controller
    {
        private JewelryShoppingEntities db = new JewelryShoppingEntities();

        // GET: Admin/Order
        public ActionResult Index(int? sort)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            IEnumerable<OrderMst> orderMsts = new List<OrderMst>();
            if (sort != null)
            {
                orderMsts = db.OrderMsts.Where(o => o.Status == sort);
            }
            else
            {
                orderMsts = db.OrderMsts.Include(o => o.UserMst);
            }
            return View(orderMsts);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMst orderMst = db.OrderMsts.Find(id);
            if (orderMst == null)
            {
                return HttpNotFound();
            }
            return View(orderMst);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            ViewBag.User_ID = new SelectList(db.UserMsts, "ID", "Username");
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,User_ID,FullName,Address,City,Country,Phone,Email,CreditCard,Status,CreatedDate")] OrderMst orderMst)
        {
            if (ModelState.IsValid)
            {
                db.OrderMsts.Add(orderMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_ID = new SelectList(db.UserMsts, "ID", "Username", orderMst.User_ID);
            return View(orderMst);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMst orderMst = db.OrderMsts.Find(id);
            if (orderMst == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_ID = new SelectList(db.UserMsts, "ID", "Username", orderMst.User_ID);
            return View(orderMst);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,User_ID,FullName,Address,City,Country,Phone,Email,CreditCard,Status,CreatedDate")] OrderMst orderMst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderMst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_ID = new SelectList(db.UserMsts, "ID", "Username", orderMst.User_ID);
            return View(orderMst);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["admin"] == null)
                return RedirectToAction("Index", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMst orderMst = db.OrderMsts.Find(id);
            if (orderMst == null)
            {
                return HttpNotFound();
            }
            return View(orderMst);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderMst orderMst = db.OrderMsts.Find(id);
            db.OrderMsts.Remove(orderMst);
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
            IEnumerable<OrderMst> orderList = new List<OrderMst>();
            orderList = db.OrderMsts.Where(x => x.FullName.Contains(key));
            return View(orderList);
        }

        [HttpPost, ActionName("ChangeStatus")]
        public ActionResult ChangeStatus(int orderId, byte status)
        {
            var order = db.OrderMsts.SingleOrDefault(o => o.ID == orderId);
            if (status == 2)
            {
                foreach (var orderDetail in order.OrderDetailMsts)
                {
                    if (orderDetail.Quantity > orderDetail.ItemMst.Quantity)
                    {
                        ViewBag.Error = "Wrong";
                        return RedirectToAction("Details", new { id = orderId });
                    }
                    orderDetail.ItemMst.Quantity -= orderDetail.Quantity;
                    db.Entry(orderDetail.ItemMst).State = EntityState.Modified;
                }
            }
            else if (order.Status > 1 && order.Status < 4 && status == 0)
            {
                foreach (var orderDetail in order.OrderDetailMsts)
                {
                    orderDetail.ItemMst.Quantity += orderDetail.Quantity;
                    db.Entry(orderDetail.ItemMst).State = EntityState.Modified;
                }
            }
            order.Status = status;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = orderId });
        }
    }
}