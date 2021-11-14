using RouletteMS.Domain.Services.Interfaces;
using RouletteMS.Infrastructure.Entities;
using RouletteMS.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Domain.Services
{
    public class RouletteService : IRouletteService<long>
    {
        private readonly IUnitOfWork _uniOfWork;
        public RouletteService(UnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        public bool Bet()
        {
            throw new NotImplementedException();
        }
        public ICollection<Bet> Close()
        {
            throw new NotImplementedException();
        }
        public long Create()
        {
            throw new NotImplementedException();
        }
        public ICollection<Roulette> GetAll()
        {
            throw new NotImplementedException();
        }
        public bool Open()
        {
            throw new NotImplementedException();
        }
    }
}
