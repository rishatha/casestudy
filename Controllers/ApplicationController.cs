using CareerConnect.DTOs;
using CareerConnect.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationController(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetAllApplications()
        {
            var applications = await _applicationRepository.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Employer,JobSeeker")]
        public async Task<ActionResult<ApplicationDTO>> GetApplicationById(int id)
        {
            var application = await _applicationRepository.GetApplicationByIdAsync(id);
            if (application == null)
                return NotFound("Application not found");

            return Ok(application);
        }

        [HttpGet("jobseeker/{jobSeekerId}")]
        [Authorize(Roles = "JobSeeker")]
        public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetApplicationsByJobSeekerId(int jobSeekerId)
        {
            var applications = await _applicationRepository.GetApplicationsByJobSeekerIdAsync(jobSeekerId);
            return Ok(applications);
        }

        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        public async Task<ActionResult<ApplicationDTO>> CreateApplication(ApplicationDTO dto)
        {
            var created = await _applicationRepository.CreateApplicationAsync(dto);
            return CreatedAtAction(nameof(GetApplicationById), new { id = created.ApplicationId }, created);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult<ApplicationDTO>> UpdateApplicationStatus(int id, [FromBody] string status)
        {
            var updated = await _applicationRepository.UpdateApplicationStatusAsync(id, status);
            if (updated == null)
                return NotFound("Application not found or inactive");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var result = await _applicationRepository.DeleteApplicationAsync(id);
            if (!result)
                return NotFound("Application not found or already inactive");

            return NoContent();
        }
    }
}
