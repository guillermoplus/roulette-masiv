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
    public class BaseProfile<Entity, Dto> : Profile
    {
        public BaseProfile()
        {
            CreateMap<Entity, Dto>();
        }
    }
}
