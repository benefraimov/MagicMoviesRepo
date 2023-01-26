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
    public class WorkersProfile : Profile
    {
        public WorkersProfile()
        {
            // source -> Target/Destination.
            CreateMap<Worker, WorkerReadDto>();
            CreateMap<WorkerCreateDto, Worker>();
            CreateMap<WorkerUpdateDto, Worker>();
            CreateMap<Worker, WorkerUpdateDto>();
        }
    }
}