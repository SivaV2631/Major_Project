using JobPortal.Areas.PortalMgmt.ViewModels;
using JobPortal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobPortal.Areas.PortalMgmt.Controllers
{
    [Area("PortalMgmt")]
    public class ShowPostJobsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ShowPostJobsController> _logger;

        public ShowPostJobsController(
            ApplicationDbContext dbContext,
            ILogger<ShowPostJobsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public IActionResult Index()
        {
            // ----- APPROACH #1 (ViewModel is sent containing the original data)
            //ShowBooksViewModel viewmodel = new ShowBooksViewModel();
            //viewmodel.Categories = _dbContext.Categories.ToList();
            //return View(viewmodel);


            // -----  APPROACH #2 (Array of SelectListItem is sent to the View)
            //List<SelectListItem> categories = new List<SelectListItem>();
            //var query = _dbContext.Categories.ToList();
            //foreach(var category in query)
            //{
            //    categories.Add(new SelectListItem(text: category.CategoryName, value: category.CategoryId.ToString()));
            //}

            PopulateDropDownListToSelectCategory();

            _logger.LogInformation("--- extracted Categories");
            return View();
        }

        private void PopulateDropDownListToSelectCategory()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem
            {
                Text = "----- select a category -----",
                Value = "",
                Selected = true
            });
            categories.AddRange(new SelectList(_dbContext.JobCategories, "JobCategoryId", "JobCategoryName"));

            ViewData["CategoriesCollection"] = categories;
        }

        // NOTE: Error messages added by Server-side code will disappear only on "SUBMIT"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("JobCategoryId")] ShowPostJobsViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDownListToSelectCategory();

                // Something is wrong with the viewmodel.  So, just return it back to the view with the ModelState errors!
                return View(viewmodel);
            }

            // Now performing server-side validation - checking if any books exist for the selected category
            bool booksExist = _dbContext.PostJobs.Any(b => b.JobCategoryId == viewmodel.JobCategoryId);
            if (!booksExist)
            {
                //--- Error will be shown as part of the Validation Summary
                ModelState.AddModelError("", "No jobs were found for the selected category!");

                //--- Error will be attached to the UI Control mapped by the asp-for attribute.
                // ModelState.AddModelError("CategoryId", "No books were found for this category");

                PopulateDropDownListToSelectCategory();
                return View(viewmodel);         // return the viewmodel with the ModelState errors!
            }

            return RedirectToAction(
                actionName: "GetPostJobsOfCategory",
                controllerName: "JobCategories",
                routeValues: new { area = "PortalMgmt", filterCategoryId = viewmodel.JobCategoryId });
        }

    }
}
