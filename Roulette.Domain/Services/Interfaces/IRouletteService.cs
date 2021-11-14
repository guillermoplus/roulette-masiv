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
        bool Open();
        ICollection<Bet> Close();
        bool Bet();
        ICollection<Roulette> GetAll();
    }
}
