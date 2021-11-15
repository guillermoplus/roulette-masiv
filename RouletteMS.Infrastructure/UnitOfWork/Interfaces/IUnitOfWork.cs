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
        IRepository<Roulette, long> RouletteRepository { get; }
        IRepository<Bet, long> BetRepository { get; }
        IRepository<Winner, long> WinnerRepository { get; }
        IRepository<User, long> UserRepository { get; }
        Task<bool> Complete();
    }
}
