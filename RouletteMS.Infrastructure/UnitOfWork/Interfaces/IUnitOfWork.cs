using RouletteMS.Infrastructure.Entities;
using RouletteMS.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Roulette, long> RouletteRepository { get; }
        IGenericRepository<Bet, long> BetRepository { get; }
        IGenericRepository<Winner, long> WinnerRepository { get; }
        IGenericRepository<User, long> UserRepository { get; }
        Task<bool> Complete();
    }
}
