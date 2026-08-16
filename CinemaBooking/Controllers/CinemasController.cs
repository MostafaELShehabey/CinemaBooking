using CinemaBooking.Application.DTOs.Cinemas;
using CinemaBooking.Application.DTOs.Screens;
using CinemaBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CinemasController : ControllerBase
{
    private readonly ICinemaService _cinemaService;
    private readonly IScreenService _screenService;

    public CinemasController(ICinemaService cinemaService, IScreenService screenService)
    {
        _cinemaService = cinemaService;
        _screenService = screenService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CinemaDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CinemaDto>>> GetAll()
    {
        return Ok(await _cinemaService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CinemaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CinemaDto>> GetById(int id)
    {
        var cinema = await _cinemaService.GetByIdAsync(id);
        return cinema is null ? NotFound() : Ok(cinema);
    }

    [HttpGet("{id:int}/screens")]
    [ProducesResponseType(typeof(IReadOnlyList<ScreenDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ScreenDto>>> GetScreens(int id)
    {
        return Ok(await _screenService.GetByCinemaIdAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CinemaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CinemaDto>> Create([FromBody] CreateCinemaDto dto)
    {
        var created = await _cinemaService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CinemaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CinemaDto>> Update(int id, [FromBody] UpdateCinemaDto dto)
    {
        return Ok(await _cinemaService.UpdateAsync(id, dto));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _cinemaService.DeleteAsync(id);
        return NoContent();
    }
}
