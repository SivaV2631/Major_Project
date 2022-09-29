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
    public class UserTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PortalMgmt/UserTables
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserTables.Include(u => u.UserType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PortalMgmt/UserTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTable = await _context.UserTables
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTable == null)
            {
                return NotFound();
            }

            return View(userTable);
        }

        // GET: PortalMgmt/UserTables/Create
        public IActionResult Create()
        {
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName");
            return View();
        }

        // POST: PortalMgmt/UserTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Password,ConfirmPassword,EmailAddress,ContactNo,Description,UniversityName,PassOutYear,Branch,Percentage,Gender,EducationDetails,UserTypeId")] UserTable userTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName", userTable.UserTypeId);
            return View(userTable);
        }

        // GET: PortalMgmt/UserTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTable = await _context.UserTables.FindAsync(id);
            if (userTable == null)
            {
                return NotFound();
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName", userTable.UserTypeId);
            return View(userTable);
        }

        // POST: PortalMgmt/UserTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Password,ConfirmPassword,EmailAddress,ContactNo,Description,UniversityName,PassOutYear,Branch,Percentage,Gender,EducationDetails,UserTypeId")] UserTable userTable)
        {
            if (id != userTable.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTableExists(userTable.UserId))
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
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "UserTypeId", "UserTypeName", userTable.UserTypeId);
            return View(userTable);
        }

        // GET: PortalMgmt/UserTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTable = await _context.UserTables
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTable == null)
            {
                return NotFound();
            }

            return View(userTable);
        }

        // POST: PortalMgmt/UserTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTable = await _context.UserTables.FindAsync(id);
            _context.UserTables.Remove(userTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTableExists(int id)
        {
            return _context.UserTables.Any(e => e.UserId == id);
        }
    }
}
