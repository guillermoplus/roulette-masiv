using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Domain.Mapper
{
    public static class MapperHelper
    {
        private static MapperConfiguration _mapperConfiguration;
        private static IMapper _mapper;
        public static MapperConfiguration GetConfig()
        {
            if (_mapperConfiguration == null)
            {
                _mapperConfiguration = new MapperConfiguration(config => config.AddProfile(typeof(MapperProfiles)));
            }
            return _mapperConfiguration;
        }
        public static IMapper GetMapper()
        {
            if (_mapper == null)
            {
                _mapper = GetConfig().CreateMapper();
            }
            return _mapper;
        }
    }
}
