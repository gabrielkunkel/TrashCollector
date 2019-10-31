using System;
using System.Linq;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomerController : Controller
    {
        public ApplicationDbContext dbContext;

        public CustomerController()
        {
            dbContext = new ApplicationDbContext();
        }


        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customer/Details/5
        // [Authorize(Roles = "Customer")]
        public ActionResult Details(string Id)
        {
            var model = dbContext.Customers.Find(Guid.Parse(Id));
            dbContext.Entry(model).Reference(a => a.Address).Load();

            return View(model);
        }

        // GET: Customer/Create
        public ActionResult Create(string Id)
        {
            var model = dbContext.Customers.Find(Guid.Parse(Id));
            return View(model);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult EditPickUp(string Id)
        {
            var model = dbContext.Customers.Find(Guid.Parse(Id));
            return View(model);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult EditPickUp(string Id, CustomerModel updateCustomer)
        {
            try
            {
                var model = dbContext.Customers.Find(Guid.Parse(Id));
                model.PickUpDay = updateCustomer.PickUpDay;

                var pickUp = dbContext.PickUps
                    .Where(pic => pic.Completed == false && pic.CustomerId == model.CustomerId && pic.Recurring == true).FirstOrDefault();

                for (int i = 1; i < 8; i++)
                {
                    DayOfWeek dayOfWeek = DateTimeOffset.UtcNow.ToLocalTime().AddDays(i).DayOfWeek;

                    if (dayOfWeek == model.PickUpDay)
                    {
                        pickUp.Scheduled = DateTimeOffset.UtcNow.ToLocalTime().AddDays(i).UtcDateTime;
                        break;
                    }
                }

                dbContext.SaveChanges();

                return RedirectToAction("Details", new { Id = model.CustomerId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
