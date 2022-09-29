using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.Data;
using JobPortal.Models;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobNaturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobNaturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobNatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobNature>>> GetJobNatures()
        {
            return await _context.JobNatures.ToListAsync();
        }

        // GET: api/JobNatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobNature>> GetJobNature(int id)
        {
            var jobNature = await _context.JobNatures.FindAsync(id);

            if (jobNature == null)
            {
                return NotFound();
            }

            return jobNature;
        }

        // PUT: api/JobNatures/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobNature(int id, JobNature jobNature)
        {
            if (id != jobNature.JobNatureId)
            {
                return BadRequest();
            }

            _context.Entry(jobNature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobNatureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/JobNatures
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<JobNature>> PostJobNature(JobNature jobNature)
        {
            _context.JobNatures.Add(jobNature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobNature", new { id = jobNature.JobNatureId }, jobNature);
        }

        // DELETE: api/JobNatures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobNature>> DeleteJobNature(int id)
        {
            var jobNature = await _context.JobNatures.FindAsync(id);
            if (jobNature == null)
            {
                return NotFound();
            }

            _context.JobNatures.Remove(jobNature);
            await _context.SaveChangesAsync();

            return jobNature;
        }

        private bool JobNatureExists(int id)
        {
            return _context.JobNatures.Any(e => e.JobNatureId == id);
        }
    }
}
