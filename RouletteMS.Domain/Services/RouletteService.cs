using AutoMapper;
using AutoMapper.QueryableExtensions;
using RouletteMS.Common;
using RouletteMS.Common.AppParameters;
using RouletteMS.Domain.Dtos;
using RouletteMS.Domain.Mapper;
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
    public class RouletteService : IRouletteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RouletteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Bet(BetDto betDto)
        {
            var bet = MapperHelper.GetMapper().Map<Bet>(betDto);
            if (await InvalidBet(bet))
            {
                return false;
            }
            _unitOfWork.BetRepository.Add(bet);
            var result = await _unitOfWork.SaveAsync();
            return result;
        }
        public async Task<IEnumerable<BetDto>> Close(long id)
        {
            var roulette = await _unitOfWork.RouletteRepository.GetAsync(id);
            if (roulette == null)
            {
                return null;
            }
            if (!roulette.IsOpen)
            {
                return null;
            }
            var bets = await _unitOfWork.BetRepository.GetWhereAsync(x => x.RouletteId == id);
            var winners = SelectWinners(bets);
            _unitOfWork.WinnerRepository.AddRange(winners);
            roulette.IsOpen = false;
            roulette.ClosingDate = DateTime.UtcNow;
            roulette.TotalAmountBet = bets.Select(x => x.Amount).Sum();
            roulette.TotalAmountDelivered = winners.Select(x => x.Amount).Sum();
            await _unitOfWork.SaveAsync();
            var betDtos = bets.AsQueryable().ProjectTo<BetDto>(MapperHelper.GetConfig()).AsEnumerable();
            return betDtos;
        }
        public async Task<long> Create()
        {
            var roulette = new Roulette
            {
                MaxAmountToBet = RouletteParameters.MAX_AMOUNT_BET
            };
            _unitOfWork.RouletteRepository.Add(roulette);
            await _unitOfWork.SaveAsync();
            return roulette.Id;
        }
        public async Task<IEnumerable<RouletteDto>> GetAll()
        {
            var roulettes = await _unitOfWork.RouletteRepository.GetAllAsync();
            var rouletteDtos = roulettes.AsQueryable()
                .ProjectTo<RouletteDto>(MapperHelper.GetConfig())
                .AsEnumerable();
            return rouletteDtos;
        }
        public async Task<bool> Open(long id)
        {
            var roulette = await _unitOfWork.RouletteRepository.GetAsync(id);
            if (roulette == null)
            {
                return false;
            }
            if (roulette.IsOpen)
            {
                return true;
            }
            roulette.IsOpen = true;
            roulette.OpeningDate = DateTime.UtcNow;
            var result = await _unitOfWork.SaveAsync();
            return result;
        }
        private IEnumerable<Winner> SelectWinners(IEnumerable<Bet> bets)
        {
            var winnerNumber = new Random().Next(RouletteParameters.ROULETTE_MIN_NUMBER, RouletteParameters.ROULETTE_MAX_NUMBER);
            var numericWinnerBets = bets.Where(x => x.Number == winnerNumber);
            var numericWinners = numericWinnerBets.Select(x => new Winner
            {
                Amount = x.Amount * RouletteParameters.WINNER_MULTIPLICATION_FACTOR,
                RouletteId = x.RouletteId,
                UserId = x.UserId
            });
            var colorWinnerBets = bets.Where(x => GetColor(x.Number) == GetColor(winnerNumber));
            var colorWinners = colorWinnerBets.Select(x => new Winner
            {
                Amount = x.Amount * RouletteParameters.COLOR_WINNER_MULTIPLICATION_FACTOR,
                RouletteId = x.RouletteId,
                UserId = x.UserId
            });
            var winners = numericWinners.Concat(colorWinners);
            return winners;
        }
        private RouletteColor.Color GetColor(int? number)
        {
            if (number % 2 == 0)
            {
                return RouletteColor.Color.Rojo;
            }
            return RouletteColor.Color.Negro;
        }
        private async Task<bool> InvalidBet(Bet bet)
        {
            var roulette = await _unitOfWork.RouletteRepository.GetAsync(bet.RouletteId);
            var maxAmountBet = roulette.MaxAmountToBet ?? RouletteParameters.MAX_AMOUNT_BET;
            return !roulette.IsOpen ||
                   bet.Amount > maxAmountBet ||
                   !(RouletteParameters.ROULETTE_MIN_NUMBER <= bet.Number && bet.Number <= RouletteParameters.ROULETTE_MAX_NUMBER) ||
                   (bet.Number == null && bet.Color.Equals(null)) ||
                   (bet.Number != null && !bet.Color.Equals(null));
        }
    }
}
