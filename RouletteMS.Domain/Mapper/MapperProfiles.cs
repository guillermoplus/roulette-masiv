using AutoMapper;
using RouletteMS.Domain.Dtos;
using RouletteMS.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Domain.Mapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<Roulette, RouletteDto>();
            CreateMap<RouletteDto, Roulette>();
            CreateMap<Bet, BetDto>();
            CreateMap<BetDto, Bet>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Winner, WinnerDto>();
            CreateMap<WinnerDto, Winner>();
        }
    }
}
