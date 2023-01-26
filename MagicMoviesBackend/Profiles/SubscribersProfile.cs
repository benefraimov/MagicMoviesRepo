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
    public class SubscribersProfile : Profile
    {
        public SubscribersProfile()
        {
            // source -> Target/Destination.SubscriberCreateDto
            CreateMap<SubscriberCreateDto, Subscriber>();
            CreateMap<SubscriberUpdateDto, Subscriber>();
            CreateMap<Subscriber, SubscriberUpdateDto>();
        }
    }
}