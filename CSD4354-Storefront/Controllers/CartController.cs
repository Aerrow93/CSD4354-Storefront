using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSD4354_Storefront.DAL;
using CSD4354_Storefront.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace CSD4354_Storefront.Controllers
{
    public class CartController : Controller
    {
        private StoreDbContext db = new StoreDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            if (this.User != null)
            {
                var appUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
                if (appUser != null)
                {
                    var userId = appUser.UserId;
                    return View(db.Carts.Where(c => c.Purchaser.Id == userId).ToList());
                }
            }
            return View(db.Carts.ToList());
        }

        // GET: Cart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            foreach (ProductQty item in cart.Items)
                db.Entry(item).Reference(i => i.Item).Load();
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Purchaser, Items, Discount, PaymentId, TaxRate, Tracking, Date, Status")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Purchaser, Items, Discount, PaymentId, TaxRate, Tracking, Date, Status")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
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

        public ActionResult AddToCart(int id, int quantity)
        {
            Product product = db.Products.Find(id);
            ProductQty item = new ProductQty();
            item.Item = product;
            item.Qty = quantity;
            var cartId = HttpContext.Session["cartId"];
            Cart cart = db.Carts.Find(cartId);
            
            if (cart == null)
            {
                var userId = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).UserId;
                var user = db.Users.Find(userId);
                cart = new Cart { Date = new DateTime(), Items = new List<ProductQty>(), Purchaser = user, Status = CartStatus.OPEN };
                db.Carts.Add(cart);
            }
            cart.Items.Add(item);
            db.SaveChanges();
            HttpContext.Session["cartId"] = cart.Id;
            return RedirectToAction("Details", new { id = cart.Id });
        }

        public ActionResult Order(int id)
        {
            return View();
        }

        public ActionResult Checkout(int id)
        {
            var cartId = HttpContext.Session["cartId"];
            Cart cart = db.Carts.Find(cartId);
            if (cart == null)
            {
                return RedirectToAction("Index");
            }
            foreach (ProductQty item in cart.Items)
                db.Entry(item).Reference(i => i.Item).Load();
            cart.Status = CartStatus.CHECKING_OUT;
            return View();
        }

        public ActionResult Pay(int id)
        {
            return View();
        }

        public ActionResult Confirm(int id)
        {

            return View();
        }

        public ActionResult Review(int id)
        {
            return View();
        }
    }
}
