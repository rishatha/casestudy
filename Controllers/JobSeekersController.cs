using CareerConnect.DTOs;
using CareerConnect.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CareerConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JobSeekersController : ControllerBase
    {
        private readonly IJobSeekerRepository _repository;

        public JobSeekersController(IJobSeekerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobSeekers = await _repository.GetAllAsync();
            return Ok(jobSeekers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var jobSeeker = await _repository.GetByIdAsync(id);
            if (jobSeeker == null) return NotFound();
            return Ok(jobSeeker);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobSeekerDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _repository.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.JobSeekerID }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JobSeekerDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _repository.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
