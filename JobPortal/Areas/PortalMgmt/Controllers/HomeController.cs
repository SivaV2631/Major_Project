using Microsoft.AspNetCore.Mvc;
using JobPortal.Areas.PortalMgmt.ViewModels;

namespace JobPortal.Areas.PortalMgmt.Controllers
{
    [Area("PortalMgmt")]         // Register the Route for the Controller to the Area
    public class HomeController : Controller
    {
        // HTTP GET
        public IActionResult DisplayJob()
        {
            CustomerViewModel viewModel = new CustomerViewModel()
            {
                CustomerId = 1,
                CreatedOn = System.DateTime.Now
            };
            return View(viewModel);
        }

        // To address over-posting ensure that the [Bind] attribute lists all the needed properties.
        // NOTE: the names of the properties is CASE-SENSITIVE.
        // HTTP POST
        [HttpPost]
        [ValidateAntiForgeryToken]                      // to address JavaScript Injection Attacks
        public IActionResult DisplayCustomer(
            [Bind("CustomerId,CustomerName,Email,Balance")] CustomerViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
