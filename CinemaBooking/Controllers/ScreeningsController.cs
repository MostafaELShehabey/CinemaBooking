using CinemaBooking.Application.DTOs.Screenings;
using CinemaBooking.Application.DTOs.Seats;
using CinemaBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ScreeningsController : ControllerBase
{
    private readonly IScreeningService _screeningService;

    public ScreeningsController(IScreeningService screeningService)
    {
        _screeningService = screeningService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ScreeningDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ScreeningDto>>> GetAll()
    {
        return Ok(await _screeningService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScreeningDto>> GetById(int id)
    {
        var screening = await _screeningService.GetByIdAsync(id);
        return screening is null ? NotFound() : Ok(screening);
    }

    [HttpGet("{id:int}/available-seats")]
    [ProducesResponseType(typeof(IReadOnlyList<SeatDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<SeatDto>>> GetAvailableSeats(int id)
    {
        return Ok(await _screeningService.GetAvailableSeatsAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScreeningDto>> Create([FromBody] CreateScreeningDto dto)
    {
        var created = await _screeningService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScreeningDto>> Update(int id, [FromBody] UpdateScreeningDto dto)
    {
        return Ok(await _screeningService.UpdateAsync(id, dto));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _screeningService.DeleteAsync(id);
        return NoContent();
    }
}
