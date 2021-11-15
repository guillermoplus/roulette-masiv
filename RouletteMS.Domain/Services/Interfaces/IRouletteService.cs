using RouletteMS.Domain.Dtos;
using RouletteMS.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Domain.Services.Interfaces
{
    public interface IRouletteService
    {
        Task<long> Create();
        Task<bool> Open(long id);
        Task<IEnumerable<BetDto>> Close(long id);
        Task<bool> Bet(BetDto bet);
        Task<IEnumerable<RouletteDto>> GetAll();
    }
}
