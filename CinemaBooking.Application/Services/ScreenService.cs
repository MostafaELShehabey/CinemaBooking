using AutoMapper;
using CinemaBooking.Application.Common;
using CinemaBooking.Application.DTOs.Screens;
using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Services;

public class ScreenService : IScreenService
{
    private readonly IScreenRepository _screenRepository;
    private readonly ICinemaRepository _cinemaRepository;
    private readonly IMapper _mapper;

    public ScreenService(
        IScreenRepository screenRepository,
        ICinemaRepository cinemaRepository,
        IMapper mapper)
    {
        _screenRepository = screenRepository;
        _cinemaRepository = cinemaRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ScreenDto>> GetAllAsync()
    {
        var screens = await _screenRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<ScreenDto>>(screens);
    }

    public async Task<ScreenDto?> GetByIdAsync(int id)
    {
        var screen = await _screenRepository.GetByIdAsync(id);
        return screen is null ? null : _mapper.Map<ScreenDto>(screen);
    }

    public async Task<IReadOnlyList<ScreenDto>> GetByCinemaIdAsync(int cinemaId)
    {
        if (!await _cinemaRepository.ExistsAsync(cinemaId))
        {
            throw new NotFoundException(nameof(Cinema), cinemaId);
        }

        var screens = await _screenRepository.GetByCinemaIdAsync(cinemaId);
        return _mapper.Map<IReadOnlyList<ScreenDto>>(screens);
    }

    public async Task<ScreenDto> CreateAsync(CreateScreenDto dto)
    {
        if (!await _cinemaRepository.ExistsAsync(dto.CinemaId))
        {
            throw new NotFoundException(nameof(Cinema), dto.CinemaId);
        }

        var screen = _mapper.Map<Screen>(dto);
        await _screenRepository.AddAsync(screen);
        await _screenRepository.SaveChangesAsync();
        return _mapper.Map<ScreenDto>(screen);
    }

    public async Task<ScreenDto> UpdateAsync(int id, UpdateScreenDto dto)
    {
        var screen = await _screenRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Screen), id);

        _mapper.Map(dto, screen);
        _screenRepository.Update(screen);
        await _screenRepository.SaveChangesAsync();
        return _mapper.Map<ScreenDto>(screen);
    }

    public async Task DeleteAsync(int id)
    {
        var screen = await _screenRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Screen), id);

        _screenRepository.Delete(screen);
        await _screenRepository.SaveChangesAsync();
    }
}
