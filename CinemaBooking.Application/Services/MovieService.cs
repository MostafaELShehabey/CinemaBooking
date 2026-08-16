using AutoMapper;
using CinemaBooking.Application.Common;
using CinemaBooking.Application.DTOs.Movies;
using CinemaBooking.Application.Interfaces;
using CinemaBooking.Domain.Entities;

namespace CinemaBooking.Application.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public MovieService(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<MovieDto>> GetAllAsync()
    {
        var movies = await _movieRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<MovieDto>>(movies);
    }

    public async Task<MovieDto?> GetByIdAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return movie is null ? null : _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDto> CreateAsync(CreateMovieDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);
        movie.IsActive = true;
        movie.CreatedAt = DateTime.UtcNow;
        await _movieRepository.AddAsync(movie);
        await _movieRepository.SaveChangesAsync();
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDto> UpdateAsync(int id, UpdateMovieDto dto)
    {
        var movie = await _movieRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Movie), id);

        _mapper.Map(dto, movie);
        _movieRepository.Update(movie);
        await _movieRepository.SaveChangesAsync();
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task DeleteAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(nameof(Movie), id);

        _movieRepository.Delete(movie);
        await _movieRepository.SaveChangesAsync();
    }
}
