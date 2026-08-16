using AutoMapper;
using CinemaBooking.Application.Common;
using CinemaBooking.Application.DTOs.Cinemas;
using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Services;

public class CinemaService : ICinemaService
{
    private readonly ICinemaRepository _cinemaRepository;
    private readonly IMapper _mapper;

    public CinemaService(ICinemaRepository cinemaRepository, IMapper mapper)
    {
        _cinemaRepository = cinemaRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CinemaDto>> GetAllAsync()
    {
        var cinemas = await _cinemaRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<CinemaDto>>(cinemas);
    }

    public async Task<CinemaDto?> GetByIdAsync(int id)
    {
        var cinema = await _cinemaRepository.GetByIdAsync(id);
        return cinema is null ? null : _mapper.Map<CinemaDto>(cinema);
    }

    public async Task<CinemaDto> CreateAsync(CreateCinemaDto dto)
    {
        var cinema = _mapper.Map<Cinema>(dto);
        await _cinemaRepository.AddAsync(cinema);
        await _cinemaRepository.SaveChangesAsync();
        return _mapper.Map<CinemaDto>(cinema);
    }

    public async Task<CinemaDto> UpdateAsync(int id, UpdateCinemaDto dto)
    {
        var cinema = await _cinemaRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Cinema), id);

        _mapper.Map(dto, cinema);
        _cinemaRepository.Update(cinema);
        await _cinemaRepository.SaveChangesAsync();
        return _mapper.Map<CinemaDto>(cinema);
    }

    public async Task DeleteAsync(int id)
    {
        var cinema = await _cinemaRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Cinema), id);

        _cinemaRepository.Delete(cinema);
        await _cinemaRepository.SaveChangesAsync();
    }
}
