using AutoMapper;
using RouletteMS.Common;
using RouletteMS.Common.AppParameters;
using RouletteMS.Domain.Dtos;
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
        private readonly IMapper _mapper;
        public RouletteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Bet(BetDto betDto)
        {
            var bet = _mapper.Map<Bet>(betDto);
            if (await InvalidBet(bet))
            {
                return false;
            }
            _unitOfWork.BetRepository.Add(bet);
            var result = await _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return result;
        }
        public async Task<IEnumerable<BetDto>> Close(long id)
        {
            var roulette = await _unitOfWork.RouletteRepository.GetAsync(id);
            roulette.IsOpen = false;
            roulette.ClosingDate = DateTime.UtcNow;
            var bets = await _unitOfWork.BetRepository.GetWhereAsync(x => x.RouletteId == id);
            SelectWinners(bets);
            await _unitOfWork.Complete();
            _unitOfWork.Dispose();
            var betDtos = _mapper.Map<IEnumerable<BetDto>>(bets);
            return betDtos;
        }
        public long Create()
        {
            var roulette = new Roulette();
            _unitOfWork.RouletteRepository.Add(roulette);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return roulette.Id;
        }
        public async Task<IEnumerable<RouletteDto>> GetAll()
        {
            var roulettes = await _unitOfWork.RouletteRepository.GetAllAsync();
            _unitOfWork.Dispose();
            var rouletteDtos = _mapper.Map<IEnumerable<RouletteDto>>(roulettes);
            return rouletteDtos;
        }
        public async Task<bool> Open(long id)
        {
            var roulette = await _unitOfWork.RouletteRepository.GetAsync(id);
            if (roulette == null) return false;
            roulette.IsOpen = true;
            roulette.OpeningDate = DateTime.UtcNow;
            var result = await _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return result;
        }
        private void SelectWinners(IEnumerable<Bet> bets)
        {
            var winnerNumber = new Random().Next(0, 36);
            var winnerBets = bets.Where(x => x.Number == winnerNumber);
            var winners = winnerBets.Select(x => new Winner
            {
                Amount = x.Amount * RouletteParameters.WINNER_MULTIPLICATION_FACTOR,
                RouletteId = x.RouletteId,
                UserId = x.UserId
            });
            _unitOfWork.WinnerRepository.AddRange(winners);
            var colorWinnerBets = bets.Where(x => GetColor(x.Number) == GetColor(winnerNumber));
            var colorWinners = colorWinnerBets.Select(x => new Winner
            {
                Amount = x.Amount * RouletteParameters.COLOR_WINNER_MULTIPLICATION_FACTOR,
                RouletteId = x.RouletteId,
                UserId = x.UserId
            });
            _unitOfWork.WinnerRepository.AddRange(colorWinners);
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
            return !roulette.IsOpen ||
                   bet.Amount > RouletteParameters.MAX_AMOUNT_BET ||
                   !(RouletteParameters.ROULETTE_MIN_NUMBER <= bet.Number && bet.Number <= RouletteParameters.ROULETTE_MAX_NUMBER) ||
                   (bet.Number == null && bet.Color.Equals(null)) ||
                   (bet.Number != null && bet.Color.Equals(null));
        }
    }
}
