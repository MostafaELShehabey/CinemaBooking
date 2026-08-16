using AutoMapper;
using CinemaBooking.Application.Common;
using CinemaBooking.Application.DTOs.Seats;
using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Services;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _seatRepository;
    private readonly IScreenRepository _screenRepository;
    private readonly IMapper _mapper;

    public SeatService(
        ISeatRepository seatRepository,
        IScreenRepository screenRepository,
        IMapper mapper)
    {
        _seatRepository = seatRepository;
        _screenRepository = screenRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<SeatDto>> GetAllAsync()
    {
        var seats = await _seatRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<SeatDto>>(seats);
    }

    public async Task<SeatDto?> GetByIdAsync(int id)
    {
        var seat = await _seatRepository.GetByIdAsync(id);
        return seat is null ? null : _mapper.Map<SeatDto>(seat);
    }

    public async Task<IReadOnlyList<SeatDto>> GetByScreenIdAsync(int screenId)
    {
        if (!await _screenRepository.ExistsAsync(screenId))
        {
            throw new NotFoundException(nameof(Screen), screenId);
        }

        var seats = await _seatRepository.GetByScreenIdAsync(screenId);
        return _mapper.Map<IReadOnlyList<SeatDto>>(seats);
    }

    public async Task<SeatDto> CreateAsync(CreateSeatDto dto)
    {
        if (!await _screenRepository.ExistsAsync(dto.ScreenId))
        {
            throw new NotFoundException(nameof(Screen), dto.ScreenId);
        }

        var seat = _mapper.Map<Seat>(dto);
        seat.IsActive = true;
        await _seatRepository.AddAsync(seat);
        await _seatRepository.SaveChangesAsync();
        return _mapper.Map<SeatDto>(seat);
    }

    public async Task<SeatDto> UpdateAsync(int id, UpdateSeatDto dto)
    {
        var seat = await _seatRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Seat), id);

        _mapper.Map(dto, seat);
        _seatRepository.Update(seat);
        await _seatRepository.SaveChangesAsync();
        return _mapper.Map<SeatDto>(seat);
    }

    public async Task DeleteAsync(int id)
    {
        var seat = await _seatRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Seat), id);

        _seatRepository.Delete(seat);
        await _seatRepository.SaveChangesAsync();
    }
}
