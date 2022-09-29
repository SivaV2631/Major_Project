using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Data;
using JobPortal.Models;

namespace JobPortal.Areas.PortalMgmt.Controllers
{
    [Area("PortalMgmt")]
    public class JobCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PortalMgmt/JobCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobCategories
                .Include(c => c.PostJobs)
                .ToListAsync());
        }

        // GET: PortalMgmt/JobCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategories
                .FirstOrDefaultAsync(m => m.JobCategoryId == id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            return View(jobCategory);
        }

        // GET: PortalMgmt/JobCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortalMgmt/JobCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobCategoryId,JobCategoryName,Description")] JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobCategory);
        }

        // GET: PortalMgmt/JobCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategories.FindAsync(id);
            if (jobCategory == null)
            {
                return NotFound();
            }
            return View(jobCategory);
        }

        // POST: PortalMgmt/JobCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobCategoryId,JobCategoryName,Description")] JobCategory jobCategory)
        {
            if (id != jobCategory.JobCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobCategoryExists(jobCategory.JobCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobCategory);
        }

        // GET: PortalMgmt/JobCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategories
                .FirstOrDefaultAsync(m => m.JobCategoryId == id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            return View(jobCategory);
        }

        // POST: PortalMgmt/JobCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobCategory = await _context.JobCategories.FindAsync(id);
            _context.JobCategories.Remove(jobCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobCategoryExists(int id)
        {
            return _context.JobCategories.Any(e => e.JobCategoryId == id);
        }

        public object GetJobCategories()
        {
            throw new NotImplementedException();
        }
    }
}
