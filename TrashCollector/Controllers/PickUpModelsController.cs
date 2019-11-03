using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class PickUpModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PickUpModels
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            EmployeeModel employee = db.Employees
                .Where(emp => emp.ApplicationId == userId)
                .FirstOrDefault();

            DateTime todayDate = DateTime.Today;

            var pickUps = db.PickUps
                .Include(p => p.Customer)
                .Where(p => p.Customer.Address.ZipCode == employee.ZipCode)
                .Where(p => p.Scheduled == todayDate)
                .ToList();
            return View(pickUps);
        }

        // GET: IndexByDay
        public ActionResult IndexByDay()
        {
            var userId = User.Identity.GetUserId();

            EmployeeModel employee = db.Employees
                .Where(emp => emp.ApplicationId == userId)
                .FirstOrDefault();

            string day = Request.QueryString["day"] ?? "Sunday";
            DayOfWeek dayForSort = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day, true);
            DateTime todayDate = GetNextWeekday(DateTime.Today, dayForSort);

            var pickUps = db.PickUps
                .Include(p => p.Customer)
                .Include(p => p.Customer.Address)
                .Where(p => p.Customer.Address.ZipCode == employee.ZipCode)
                .Where(p => p.Scheduled == todayDate)
                .ToList();
            return View("Index", pickUps);
        }

        private DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        // GET: PickUpModels/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUpModel pickUpModel = db.PickUps
                .Include(pick => pick.Customer)
                .Include(pick => pick.Customer.Address)
                .Where(pick => pick.PickUpId == id)
                .SingleOrDefault();

            if (pickUpModel == null)
            {
                return HttpNotFound();
            }
            return View(pickUpModel);
        }

        // GET: PickUpModels/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
            return View();
        }

        // POST: PickUpModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PickUpId,Scheduled,Pending,Completed,Recurring,Cost,Paid,CustomerId")] PickUpModel pickUpModel)
        {
            if (ModelState.IsValid)
            {
                pickUpModel.PickUpId = Guid.NewGuid();
                db.PickUps.Add(pickUpModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", pickUpModel.CustomerId);
            return View(pickUpModel);
        }

        // GET: PickUpModels/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUpModel pickUpModel = db.PickUps.Find(id);
            if (pickUpModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", pickUpModel.CustomerId);
            return View(pickUpModel);
        }

        // POST: PickUpModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PickUpId,Scheduled,Pending,Completed,Recurring,Cost,Paid,CustomerId")] PickUpModel pickUpModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickUpModel).State = EntityState.Modified;
                db.SaveChanges();

                var userId = User.Identity.GetUserId();

                EmployeeModel employee = db.Employees
                    .Where(emp => emp.ApplicationId == userId)
                    .FirstOrDefault();

                return RedirectToAction("Index", new { Id = employee.EmployeeId });
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", pickUpModel.CustomerId);
            return View(pickUpModel);
        }

        // GET: PickUpModels/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUpModel pickUpModel = db.PickUps.Find(id);
            if (pickUpModel == null)
            {
                return HttpNotFound();
            }
            return View(pickUpModel);
        }

        // POST: PickUpModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PickUpModel pickUpModel = db.PickUps.Find(id);
            db.PickUps.Remove(pickUpModel);
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
    }
}
