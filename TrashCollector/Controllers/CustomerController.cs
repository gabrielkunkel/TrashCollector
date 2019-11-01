using System;
using System.Globalization;
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
        public ActionResult CreateAddPickUp(string Id)
        {
            var customerModel = dbContext.Customers.Find(Guid.Parse(Id));

            var model = new PickUpModel
            {
                PickUpId = Guid.NewGuid(),
                Scheduled = DateTimeOffset.UtcNow.ToLocalTime().UtcDateTime,
                Pending = true,
                Completed = false,
                Recurring = false,
                Cost = customerModel.BaseCost,
                Paid = false,
                CustomerId = Guid.Parse(Id)
            };

            return View(model);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult CreateAddPickUp(PickUpModel pickUp)
        {
            try
            {
                dbContext.PickUps.Add(pickUp);
                dbContext.SaveChanges();

                return RedirectToAction("Details", "Customer", new { Id = pickUp.CustomerId });
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

                // todo: consider simplifying this solution with DateTime arithematic
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

        public ActionResult OwesThisMonth(string Id)
        {
            var customer = dbContext.Customers.Find(Guid.Parse(Id));

            DateTime firstPickUpDayForThisMonthSubtractOne = dbContext.PickUps
                .Where(pick => pick.CustomerId == customer.CustomerId && pick.Recurring == true && pick.Scheduled.Month == DateTime.Now.Month)
                .OrderBy(pick => pick.Scheduled)
                .Select(pick => pick.Scheduled)
                .FirstOrDefault()
                .AddDays(-1);
            DateTime lastDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));

            int daysToChargeFor = CountDays(customer.PickUpDay, firstPickUpDayForThisMonthSubtractOne, lastDayOfMonth);

            decimal amountForRecurring = daysToChargeFor * customer.BaseCost;

            decimal totalForAdditional = dbContext.PickUps
                .Where(pick => pick.CustomerId == customer.CustomerId && pick.Recurring == false && pick.Scheduled.Month == DateTime.Now.Month)
                .Sum(pick => pick.Cost);

            decimal totalCost = totalForAdditional + amountForRecurring;

            CustomerOwesForThisMonthViewModel model = new CustomerOwesForThisMonthViewModel
            {
                CustomerId = customer.CustomerId,
                OwesThisMonth = totalCost
            };

            return View(model);
        }

        private int CountDays(DayOfWeek day, DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;                       // Total duration
            int count = (int)Math.Floor(ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int)(ts.TotalDays % 7);         // Number of remaining days
            int sinceLastDay = (int)(end.DayOfWeek - day);   // Number of days since last [day]
            if (sinceLastDay < 0) sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if (remainder >= sinceLastDay) count++;

            return count;
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
