using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CareerConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apps = await _applicationService.GetAllApplicationsAsync();
            return Ok(apps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var app = await _applicationService.GetApplicationByIdAsync(id);
            if (app == null) return NotFound();
            return Ok(app);
        }

        [HttpGet("jobseeker/{jobSeekerId}")]
        public async Task<IActionResult> GetByJobSeeker(int jobSeekerId)
        {
            var apps = await _applicationService.GetApplicationsByJobSeekerIdAsync(jobSeekerId);
            return Ok(apps);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _applicationService.CreateApplicationAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ApplicationId }, result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var result = await _applicationService.UpdateApplicationStatusAsync(id, status);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _applicationService.DeleteApplicationAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
