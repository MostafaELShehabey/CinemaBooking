using CinemaBooking.Application.DTOs.Screenings;
using CinemaBooking.Application.DTOs.Screens;
using CinemaBooking.Application.DTOs.Seats;
using CinemaBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ScreensController : ControllerBase
{
    private readonly IScreenService _screenService;
    private readonly ISeatService _seatService;
    private readonly IScreeningService _screeningService;

    public ScreensController(
        IScreenService screenService,
        ISeatService seatService,
        IScreeningService screeningService)
    {
        _screenService = screenService;
        _seatService = seatService;
        _screeningService = screeningService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ScreenDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ScreenDto>>> GetAll()
    {
        return Ok(await _screenService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ScreenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScreenDto>> GetById(int id)
    {
        var screen = await _screenService.GetByIdAsync(id);
        return screen is null ? NotFound() : Ok(screen);
    }

    [HttpGet("{id:int}/seats")]
    [ProducesResponseType(typeof(IReadOnlyList<SeatDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<SeatDto>>> GetSeats(int id)
    {
        return Ok(await _seatService.GetByScreenIdAsync(id));
    }

    [HttpGet("{id:int}/screenings")]
    [ProducesResponseType(typeof(IReadOnlyList<ScreeningDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ScreeningDto>>> GetScreenings(int id)
    {
        return Ok(await _screeningService.GetByScreenIdAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ScreenDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScreenDto>> Create([FromBody] CreateScreenDto dto)
    {
        var created = await _screenService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ScreenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScreenDto>> Update(int id, [FromBody] UpdateScreenDto dto)
    {
        return Ok(await _screenService.UpdateAsync(id, dto));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _screenService.DeleteAsync(id);
        return NoContent();
    }
}
