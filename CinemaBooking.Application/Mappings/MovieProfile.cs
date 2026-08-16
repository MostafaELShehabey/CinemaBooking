using AutoMapper;
using CinemaBooking.Application.DTOs.Movies;
using CinemaBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaBooking.Application.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();
        }
    }
}
