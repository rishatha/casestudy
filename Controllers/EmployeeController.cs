using CareerConnect.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployerController : ControllerBase
{
    private readonly IEmployerService _service;

    public EmployerController(IEmployerService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<EmployerDTO>> CreateEmployer([FromBody] EmployerDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.CreateEmployerAsync(dto);
        return CreatedAtAction(nameof(GetEmployerById), new { id = result.EmployerId }, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployerDTO>> GetEmployerById(int id)
    {
        var emp = await _service.GetEmployerByIdAsync(id);
        if (emp == null) return NotFound();
        return Ok(emp);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployerDTO>>> GetAllEmployers()
    {
        return Ok(await _service.GetAllEmployersAsync());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEmployer(int id, [FromBody] EmployerDTO dto)
    {
        var result = await _service.UpdateEmployerAsync(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployer(int id)
    {
        var success = await _service.DeleteEmployerAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
