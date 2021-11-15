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
        private IGenericRepository<Roulette, long> rouletteRepository;
        private IGenericRepository<Bet, long> betRepository;
        private IGenericRepository<Winner, long> winnerRepository;
        private IGenericRepository<User, long> userRepository;
        public IGenericRepository<Roulette, long> RouletteRepository => rouletteRepository ?? new GenericRepository<Roulette, long>(_context);
        public IGenericRepository<Bet, long> BetRepository => betRepository ?? new GenericRepository<Bet, long>(_context);
        public IGenericRepository<Winner, long> WinnerRepository => winnerRepository ?? new GenericRepository<Winner, long>(_context);
        public IGenericRepository<User, long> UserRepository => userRepository ?? new GenericRepository<User, long>(_context);
        #endregion
        #region Constructor
        private readonly RouletteContext _context;
        private bool disposed = false;
        public UnitOfWork(RouletteContext context)
        {
            _context = context;
        }
        #endregion
        #region Methods
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
