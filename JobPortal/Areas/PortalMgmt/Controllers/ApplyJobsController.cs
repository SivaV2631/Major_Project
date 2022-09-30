using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Data;
using JobPortal.Models;
using Microsoft.Extensions.Logging;

namespace JobPortal.Areas.PortalMgmt.Controllers
{
    [Area("PortalMgmt")]
    public class ApplyJobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApplyJobsController> _logger;

        public ApplyJobsController(ApplicationDbContext context, ILogger<ApplyJobsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: PortalMgmt/ApplyJobs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplyJobs.Include(a => a.PostJob);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PortalMgmt/ApplyJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyJob = await _context.ApplyJobs
                .Include(a => a.PostJob)
                .FirstOrDefaultAsync(m => m.ApplyJobId == id);
            if (applyJob == null)
            {
                return NotFound();
            }

            return View(applyJob);
        }

        // GET: PortalMgmt/ApplyJobs/Create
        public IActionResult Create()
        {
           
            ViewData["PostJobId"] = new SelectList(_context.PostJobs, "PostJobId", "JobTitle");
            return View();
        }

        // POST: PortalMgmt/ApplyJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplyJobId,ApplicantName,ContactNo,Email,AppliedAt,PostJobId")] ApplyJob applyJob)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(applyJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            ViewData["PostJobId"] = new SelectList(_context.PostJobs, "PostJobId", "JobTitle", applyJob.PostJobId);
            return View(applyJob);
        }

        // GET: PortalMgmt/ApplyJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyJob = await _context.ApplyJobs.FindAsync(id);
            if (applyJob == null)
            {
                return NotFound();
            }
            ViewData["PostJobId"] = new SelectList(_context.PostJobs, "PostJobId", "JobTitle", applyJob.PostJobId);
            return View(applyJob);
        }

        // POST: PortalMgmt/ApplyJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplyJobId,ApplicantName,ContactNo,Email,AppliedAt,PostJobId")] ApplyJob applyJob)
        {
            if (id != applyJob.ApplyJobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applyJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplyJobExists(applyJob.ApplyJobId))
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
            ViewData["PostJobId"] = new SelectList(_context.PostJobs, "PostJobId", "JobTitle", applyJob.PostJobId);
            return View(applyJob);
        }

        // GET: PortalMgmt/ApplyJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyJob = await _context.ApplyJobs
                .Include(a => a.PostJob)
                .FirstOrDefaultAsync(m => m.ApplyJobId == id);
            if (applyJob == null)
            {
                return NotFound();
            }

            return View(applyJob);
        }

        // POST: PortalMgmt/ApplyJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applyJob = await _context.ApplyJobs.FindAsync(id);
            _context.ApplyJobs.Remove(applyJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplyJobExists(int id)
        {
            return _context.ApplyJobs.Any(e => e.ApplyJobId == id);
        }
    }
}
