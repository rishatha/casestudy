using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareerConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;

        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobRepository.GetAllJobsAsync();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            if (job == null) return NotFound();
            return Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] JobDTO jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var newJob = await _jobRepository.CreateJobAsync(jobDto);
            return CreatedAtAction(nameof(GetJobById), new { id = newJob.JobId }, newJob);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] JobDTO jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedJob = await _jobRepository.UpdateJobAsync(id, jobDto);
            if (updatedJob == null) return NotFound();
            return Ok(updatedJob);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var result = await _jobRepository.DeleteJobAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
