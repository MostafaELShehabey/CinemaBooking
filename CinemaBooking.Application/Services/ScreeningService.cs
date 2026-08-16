using AutoMapper;
using CinemaBooking.Application.Common;
using CinemaBooking.Application.DTOs.Screenings;
using CinemaBooking.Application.DTOs.Seats;
using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Services;

public class ScreeningService : IScreeningService
{
    private readonly IScreeningRepository _screeningRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IScreenRepository _screenRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IBookingSeatRepository _bookingSeatRepository;
    private readonly IMapper _mapper;

    public ScreeningService(
        IScreeningRepository screeningRepository,
        IMovieRepository movieRepository,
        IScreenRepository screenRepository,
        ISeatRepository seatRepository,
        IBookingSeatRepository bookingSeatRepository,
        IMapper mapper)
    {
        _screeningRepository = screeningRepository;
        _movieRepository = movieRepository;
        _screenRepository = screenRepository;
        _seatRepository = seatRepository;
        _bookingSeatRepository = bookingSeatRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ScreeningDto>> GetAllAsync()
    {
        var screenings = await _screeningRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<ScreeningDto>>(screenings);
    }

    public async Task<ScreeningDto?> GetByIdAsync(int id)
    {
        var screening = await _screeningRepository.GetByIdAsync(id);
        return screening is null ? null : _mapper.Map<ScreeningDto>(screening);
    }

    public async Task<IReadOnlyList<ScreeningDto>> GetByMovieIdAsync(int movieId)
    {
        if (!await _movieRepository.ExistsAsync(movieId))
        {
            throw new NotFoundException(nameof(Movie), movieId);
        }

        var screenings = await _screeningRepository.GetByMovieIdAsync(movieId);
        return _mapper.Map<IReadOnlyList<ScreeningDto>>(screenings);
    }

    public async Task<IReadOnlyList<ScreeningDto>> GetByScreenIdAsync(int screenId)
    {
        if (!await _screenRepository.ExistsAsync(screenId))
        {
            throw new NotFoundException(nameof(Screen), screenId);
        }

        var screenings = await _screeningRepository.GetByScreenIdAsync(screenId);
        return _mapper.Map<IReadOnlyList<ScreeningDto>>(screenings);
    }

    public async Task<IReadOnlyList<SeatDto>> GetAvailableSeatsAsync(int screeningId)
    {
        var screening = await _screeningRepository.GetByIdAsync(screeningId)
            ?? throw new NotFoundException(nameof(Screening), screeningId);

        var seats = await _seatRepository.GetByScreenIdAsync(screening.ScreenId);
        var bookedSeats = await _bookingSeatRepository.GetByScreeningIdAsync(screeningId);
        var bookedSeatIds = bookedSeats.Select(bs => bs.SeatId).ToHashSet();

        var available = seats
            .Where(s => s.IsActive && !bookedSeatIds.Contains(s.Id))
            .ToList();

        return _mapper.Map<IReadOnlyList<SeatDto>>(available);
    }

    public async Task<ScreeningDto> CreateAsync(CreateScreeningDto dto)
    {
        ValidateTimesAndPrice(dto.StartTime, dto.EndTime, dto.Price);

        var movie = await _movieRepository.GetByIdAsync(dto.MovieId)
            ?? throw new NotFoundException(nameof(Movie), dto.MovieId);

        if (!movie.IsActive)
        {
            throw new BusinessRuleException("Inactive movies cannot be scheduled.");
        }

        if (!await _screenRepository.ExistsAsync(dto.ScreenId))
        {
            throw new NotFoundException(nameof(Screen), dto.ScreenId);
        }

        if (await _screeningRepository.HasOverlapAsync(dto.ScreenId, dto.StartTime, dto.EndTime))
        {
            throw new BusinessRuleException("This screen already has a screening in that time range.");
        }

        var screening = _mapper.Map<Screening>(dto);
        screening.IsActive = true;
        await _screeningRepository.AddAsync(screening);
        await _screeningRepository.SaveChangesAsync();
        return _mapper.Map<ScreeningDto>(screening);
    }

    public async Task<ScreeningDto> UpdateAsync(int id, UpdateScreeningDto dto)
    {
        ValidateTimesAndPrice(dto.StartTime, dto.EndTime, dto.Price);

        var screening = await _screeningRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Screening), id);

        if (await _screeningRepository.HasOverlapAsync(
                screening.ScreenId,
                dto.StartTime,
                dto.EndTime,
                screening.Id))
        {
            throw new BusinessRuleException("This screen already has a screening in that time range.");
        }

        _mapper.Map(dto, screening);
        _screeningRepository.Update(screening);
        await _screeningRepository.SaveChangesAsync();
        return _mapper.Map<ScreeningDto>(screening);
    }

    public async Task DeleteAsync(int id)
    {
        var screening = await _screeningRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Screening), id);

        _screeningRepository.Delete(screening);
        await _screeningRepository.SaveChangesAsync();
    }

    private static void ValidateTimesAndPrice(DateTime startTime, DateTime endTime, decimal price)
    {
        if (endTime <= startTime)
        {
            throw new BusinessRuleException("Screening end time must be after the start time.");
        }

        if (price <= 0)
        {
            throw new BusinessRuleException("Screening price must be greater than zero.");
        }
    }
}
