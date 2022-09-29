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
    public class JobNaturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobNaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PortalMgmt/JobNatures
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobNatures.ToListAsync());
        }

        // GET: PortalMgmt/JobNatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobNature = await _context.JobNatures
                .FirstOrDefaultAsync(m => m.JobNatureId == id);
            if (jobNature == null)
            {
                return NotFound();
            }

            return View(jobNature);
        }

        // GET: PortalMgmt/JobNatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortalMgmt/JobNatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobNatureId,JobNatureName")] JobNature jobNature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobNature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobNature);
        }

        // GET: PortalMgmt/JobNatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobNature = await _context.JobNatures.FindAsync(id);
            if (jobNature == null)
            {
                return NotFound();
            }
            return View(jobNature);
        }

        // POST: PortalMgmt/JobNatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobNatureId,JobNatureName")] JobNature jobNature)
        {
            if (id != jobNature.JobNatureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobNature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobNatureExists(jobNature.JobNatureId))
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
            return View(jobNature);
        }

        // GET: PortalMgmt/JobNatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobNature = await _context.JobNatures
                .FirstOrDefaultAsync(m => m.JobNatureId == id);
            if (jobNature == null)
            {
                return NotFound();
            }

            return View(jobNature);
        }

        // POST: PortalMgmt/JobNatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobNature = await _context.JobNatures.FindAsync(id);
            _context.JobNatures.Remove(jobNature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobNatureExists(int id)
        {
            return _context.JobNatures.Any(e => e.JobNatureId == id);
        }
    }
}
