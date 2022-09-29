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
    public class JobRequirementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobRequirementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PortalMgmt/JobRequirements
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobRequirements.ToListAsync());
        }

        // GET: PortalMgmt/JobRequirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRequirement = await _context.JobRequirements
                .FirstOrDefaultAsync(m => m.JobRequirementId == id);
            if (jobRequirement == null)
            {
                return NotFound();
            }

            return View(jobRequirement);
        }

        // GET: PortalMgmt/JobRequirements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortalMgmt/JobRequirements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobRequirementId,JobRequirementTitle,JobRequirementDescription")] JobRequirement jobRequirement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobRequirement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobRequirement);
        }

        // GET: PortalMgmt/JobRequirements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRequirement = await _context.JobRequirements.FindAsync(id);
            if (jobRequirement == null)
            {
                return NotFound();
            }
            return View(jobRequirement);
        }

        // POST: PortalMgmt/JobRequirements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobRequirementId,JobRequirementTitle,JobRequirementDescription")] JobRequirement jobRequirement)
        {
            if (id != jobRequirement.JobRequirementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobRequirement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobRequirementExists(jobRequirement.JobRequirementId))
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
            return View(jobRequirement);
        }

        // GET: PortalMgmt/JobRequirements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRequirement = await _context.JobRequirements
                .FirstOrDefaultAsync(m => m.JobRequirementId == id);
            if (jobRequirement == null)
            {
                return NotFound();
            }

            return View(jobRequirement);
        }

        // POST: PortalMgmt/JobRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobRequirement = await _context.JobRequirements.FindAsync(id);
            _context.JobRequirements.Remove(jobRequirement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobRequirementExists(int id)
        {
            return _context.JobRequirements.Any(e => e.JobRequirementId == id);
        }
    }
}
