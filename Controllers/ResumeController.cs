using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CareerConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All actions require authentication
    public class ResumeController : ControllerBase
    {
        private readonly IResumeRepository _resumeRepository;

        public ResumeController(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        // Accessible by both Employer and JobSeeker (optional)
        [HttpGet]
        [Authorize(Roles = "Employer,JobSeeker")]
        public async Task<IActionResult> GetAll()
        {
            var resumes = await _resumeRepository.GetAllResumesAsync();
            return Ok(resumes);
        }

        // Accessible by both Employer and JobSeeker
        [HttpGet("{id}")]
        [Authorize(Roles = "Employer,JobSeeker")]
        public async Task<IActionResult> GetById(int id)
        {
            var resume = await _resumeRepository.GetResumeByIdAsync(id);
            if (resume == null) return NotFound();
            return Ok(resume);
        }

        // Only JobSeeker can view their own resumes
        [HttpGet("jobseeker/{jobSeekerId}")]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> GetByJobSeeker(int jobSeekerId)
        {
            var resumes = await _resumeRepository.GetResumesByJobSeekerIdAsync(jobSeekerId);
            return Ok(resumes);
        }

        // Only JobSeeker can create a resume
        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Create([FromBody] ResumeDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _resumeRepository.CreateResumeAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ResumeId }, result);
        }

        // Only JobSeeker can delete their own resume
        [HttpDelete("{id}")]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _resumeRepository.DeleteResumeAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
