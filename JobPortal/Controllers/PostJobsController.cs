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
    public class PostJobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostJobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PostJobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostJob>>> GetPostJobs()
        {
            return await _context.PostJobs.ToListAsync();
        }

        // GET: api/PostJobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostJob>> GetPostJob(int id)
        {
            var postJob = await _context.PostJobs.FindAsync(id);

            if (postJob == null)
            {
                return NotFound();
            }

            return postJob;
        }

        // PUT: api/PostJobs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostJob(int id, PostJob postJob)
        {
            if (id != postJob.PostJobId)
            {
                return BadRequest();
            }

            _context.Entry(postJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostJobExists(id))
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

        // POST: api/PostJobs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PostJob>> PostPostJob(PostJob postJob)
        {
            _context.PostJobs.Add(postJob);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostJob", new { id = postJob.PostJobId }, postJob);
        }

        // DELETE: api/PostJobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostJob>> DeletePostJob(int id)
        {
            var postJob = await _context.PostJobs.FindAsync(id);
            if (postJob == null)
            {
                return NotFound();
            }

            _context.PostJobs.Remove(postJob);
            await _context.SaveChangesAsync();

            return postJob;
        }

        private bool PostJobExists(int id)
        {
            return _context.PostJobs.Any(e => e.PostJobId == id);
        }
    }
}
