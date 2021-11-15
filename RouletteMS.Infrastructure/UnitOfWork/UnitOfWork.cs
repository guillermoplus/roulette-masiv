using RouletteMS.Infrastructure.DataContext;
using RouletteMS.Infrastructure.Entities;
using RouletteMS.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Repositories
        private IRepository<Roulette, long> _rouletteRepository;
        private IRepository<Bet, long> _betRepository;
        private IRepository<Winner, long> _winnerRepository;
        private IRepository<User, long> _userRepository;
        public IRepository<Roulette, long> RouletteRepository => _rouletteRepository ?? new Repository<Roulette, long>(_context);
        public IRepository<Bet, long> BetRepository => _betRepository ?? new Repository<Bet, long>(_context);
        public IRepository<Winner, long> WinnerRepository => _winnerRepository ?? new Repository<Winner, long>(_context);
        public IRepository<User, long> UserRepository => _userRepository ?? new Repository<User, long>(_context);
        #endregion
        #region Constructor
        private readonly RouletteContext _context;
        public UnitOfWork(RouletteContext context)
        {
            _context = context;
        }
        #endregion
        #region Methods
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}
