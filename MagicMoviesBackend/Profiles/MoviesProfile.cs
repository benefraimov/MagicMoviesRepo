using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using MagicMoviesBackend.Dtos;
using MagicMoviesBackend.Models;

namespace MagicMoviesBackend.Profiles
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            // source -> Target/Destination
            CreateMap<MovieUpdateDto, Movie>();
            CreateMap<Movie, MovieUpdateDto>();
        }
    }
}