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
    public class ApplyJobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplyJobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApplyJobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplyJob>>> GetApplyJobs()
        {
            return await _context.ApplyJobs.ToListAsync();
        }

        // GET: api/ApplyJobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplyJob>> GetApplyJob(int id)
        {
            var applyJob = await _context.ApplyJobs.FindAsync(id);

            if (applyJob == null)
            {
                return NotFound();
            }

            return applyJob;
        }

        // PUT: api/ApplyJobs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplyJob(int id, ApplyJob applyJob)
        {
            if (id != applyJob.ApplyJobId)
            {
                return BadRequest();
            }

            _context.Entry(applyJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplyJobExists(id))
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

        // POST: api/ApplyJobs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ApplyJob>> PostApplyJob(ApplyJob applyJob)
        {
            try
            {
                _context.ApplyJobs.Add(applyJob);
                await _context.SaveChangesAsync();

                // To enforce that the HTTP RESPONSE CODE 201 "CREATED", package the response.
                var result = CreatedAtAction("GetApplyJob", new { id = applyJob.ApplyJobId }, applyJob);
                return Ok(result);
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("POST", exp.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/ApplyJobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplyJob>> DeleteApplyJob(int id)
        {

            try
            {
                var applyJob = await _context.ApplyJobs.FindAsync(id);
                if (applyJob == null)
                {
                    return NotFound();
                }
                _context.ApplyJobs.Remove(applyJob);
                await _context.SaveChangesAsync();

                return Ok(applyJob);
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);
            }
        }

        private bool ApplyJobExists(int id)
        {
            return _context.ApplyJobs.Any(e => e.ApplyJobId == id);
        }
    }
}
