using RouletteMS.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Domain.Services.Interfaces
{
    public interface IRouletteService<IdType> where IdType : struct
    {
        IdType Create();
        Task<bool> Open(IdType id);
        Task<IEnumerable<Bet>> Close(IdType id);
        bool Bet(Bet bet);
        Task<IEnumerable<Roulette>> GetAll();
    }
}
