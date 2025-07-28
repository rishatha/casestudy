using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CareerConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeRepository _resumeRepository;

        public ResumeController(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resumes = await _resumeRepository.GetAllResumesAsync();
            return Ok(resumes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resume = await _resumeRepository.GetResumeByIdAsync(id);
            if (resume == null) return NotFound();
            return Ok(resume);
        }

        [HttpGet("jobseeker/{jobSeekerId}")]
        public async Task<IActionResult> GetByJobSeeker(int jobSeekerId)
        {
            var resumes = await _resumeRepository.GetResumesByJobSeekerIdAsync(jobSeekerId);
            return Ok(resumes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResumeDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _resumeRepository.CreateResumeAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ResumeId }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _resumeRepository.DeleteResumeAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
