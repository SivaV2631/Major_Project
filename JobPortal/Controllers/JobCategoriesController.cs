using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.Data;
using JobPortal.Models;
using Microsoft.Extensions.Logging;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<JobCategoriesController> _logger;

        public JobCategoriesController(ApplicationDbContext context, ILogger<JobCategoriesController> logger)
        {
            _context = context;
            _logger = logger;

        }

        // GET: api/JobCategories
        [HttpGet]
        public async Task<IActionResult> GetJobCategories()
        {
            try
            {

                var categories =  await _context.JobCategories
                    .Include(c => c.PostJobs)
                    .ToListAsync();
                if (categories == null)
                {
                    return NotFound();          // RETURN: No data was found            HTTP 404
                }
                
                return Ok(categories);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message); // RETURN: BadResult                    HTTP 400
            }
        }

        // GET: api/JobCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobCategory(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var jobcategory = await _context.JobCategories.FindAsync(id);

                if (jobcategory == null)
                {
                    return NotFound();
                }

                return Ok(jobcategory);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        // PUT: api/JobCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobCategory(int id, JobCategory jobCategory)
        {

            if (id != jobCategory.JobCategoryId)
            {
                return BadRequest();
            }

            //sanitize the data
            jobCategory.JobCategoryName = jobCategory.JobCategoryName.Trim();

            //server side validation
            bool isDuplicateFound = _context.JobCategories.Any(c => c.JobCategoryName == jobCategory.JobCategoryName);
            if(isDuplicateFound)
            {
                ModelState.AddModelError("POST", "Duplicate Category Found!");
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Entry(jobCategory).State = EntityState.Modified;
                     
                    await _context.SaveChangesAsync();
                    return NoContent();
                     
                }
                catch (DbUpdateConcurrencyException exp)
                {
                    if (!JobCategoryExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("PUT", exp.Message);
                    }
                }
            }
     return BadRequest(ModelState);
        }

        // POST: api/JobCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostJobCategory(JobCategory jobCategory)
        {
            //_context.JobCategories.Add(jobCategory);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetJobCategory", new { id = jobCategory.JobCategoryId }, jobCategory);

            try
            {
                _context.JobCategories.Add(jobCategory);
                await _context.SaveChangesAsync();

                // return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);

                // To enforce that the HTTP RESPONSE CODE 201 "CREATED", package the response.
                var result = CreatedAtAction("GetJobCategory", new { id = jobCategory.JobCategoryId }, jobCategory);
                return Ok(result);
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("POST", exp.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/JobCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobCategory(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var jobCategory = await _context.JobCategories.FindAsync(id);
                if (jobCategory == null)
                {
                    return NotFound();
                }
                _context.JobCategories.Remove(jobCategory);
                await _context.SaveChangesAsync();

                return Ok(jobCategory);
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);
            }




        }

        private bool JobCategoryExists(int id)
        {
            return _context.JobCategories.Any(e => e.JobCategoryId == id);
        }
    }
}
