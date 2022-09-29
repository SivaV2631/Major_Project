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
    public class JobRequirementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobRequirementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobRequirements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobRequirement>>> GetJobRequirements()
        {
            return await _context.JobRequirements.ToListAsync();
        }

        // GET: api/JobRequirements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobRequirement>> GetJobRequirement(int id)
        {
            var jobRequirement = await _context.JobRequirements.FindAsync(id);

            if (jobRequirement == null)
            {
                return NotFound();
            }

            return jobRequirement;
        }

        // PUT: api/JobRequirements/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobRequirement(int id, JobRequirement jobRequirement)
        {
            if (id != jobRequirement.JobRequirementId)
            {
                return BadRequest();
            }

            _context.Entry(jobRequirement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobRequirementExists(id))
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

        // POST: api/JobRequirements
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<JobRequirement>> PostJobRequirement(JobRequirement jobRequirement)
        {
            _context.JobRequirements.Add(jobRequirement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobRequirement", new { id = jobRequirement.JobRequirementId }, jobRequirement);
        }

        // DELETE: api/JobRequirements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobRequirement>> DeleteJobRequirement(int id)
        {
            var jobRequirement = await _context.JobRequirements.FindAsync(id);
            if (jobRequirement == null)
            {
                return NotFound();
            }

            _context.JobRequirements.Remove(jobRequirement);
            await _context.SaveChangesAsync();

            return jobRequirement;
        }

        private bool JobRequirementExists(int id)
        {
            return _context.JobRequirements.Any(e => e.JobRequirementId == id);
        }
    }
}
