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
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerRepository _repository;

        public EmployerController(IEmployerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployer([FromBody] EmployerDTO dto)
        {
            var result = await _repository.CreateEmployerAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployer(int id)
        {
            var result = await _repository.GetEmployerByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployers()
        {
            var result = await _repository.GetAllEmployersAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployer(int id, [FromBody] EmployerDTO dto)
        {
            var result = await _repository.UpdateEmployerAsync(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            var success = await _repository.DeleteEmployerAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
