using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext dbContext;

        public HomeController()
        {
            dbContext = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Customer"))
            {
                var userId = User.Identity.GetUserId();
                var CustomerId = dbContext.Customers.Where(cust => cust.ApplicationId == userId).Select(cust => cust.CustomerId).SingleOrDefault().ToString();
                return RedirectToAction("Details", "Customer", new { Id = CustomerId });
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}