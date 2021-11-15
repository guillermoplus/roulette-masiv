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
        private IGenericRepository<Roulette, long> _rouletteRepository;
        private IGenericRepository<Bet, long> _betRepository;
        private IGenericRepository<Winner, long> _winnerRepository;
        private IGenericRepository<User, long> _userRepository;
        public IGenericRepository<Roulette, long> RouletteRepository => _rouletteRepository ?? new GenericRepository<Roulette, long>(_context);
        public IGenericRepository<Bet, long> BetRepository => _betRepository ?? new GenericRepository<Bet, long>(_context);
        public IGenericRepository<Winner, long> WinnerRepository => _winnerRepository ?? new GenericRepository<Winner, long>(_context);
        public IGenericRepository<User, long> UserRepository => _userRepository ?? new GenericRepository<User, long>(_context);
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
