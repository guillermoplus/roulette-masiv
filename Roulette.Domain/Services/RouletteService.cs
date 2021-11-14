using RouletteMS.Common;
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
        private readonly double MAX_AMOUNT_BET = double.Parse(Environment.GetEnvironmentVariable("MAX_AMOUNT_BET"));
        private readonly double WINNER_MULTIPLICATION_FACTOR = double.Parse(Environment.GetEnvironmentVariable("WINNER_MULTIPLICATION_FACTOR"));
        private readonly double COLOR_WINNER_MULTIPLICATION_FACTOR = double.Parse(Environment.GetEnvironmentVariable("COLOR_WINNER_MULTIPLICATION_FACTOR"));
        private readonly double ROULETTE_MAX_NUMBER = double.Parse(Environment.GetEnvironmentVariable("ROULETTE_MAX_NUMBER"));
        private readonly double ROULETTE_MIN_NUMBER = double.Parse(Environment.GetEnvironmentVariable("ROULETTE_MIN_NUMBER"));
        private readonly IUnitOfWork _unitOfWork;
        public RouletteService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Bet(Bet bet)
        {
            if (await InvalidBet(bet))
            {
                return false;
            }
            _unitOfWork.BetRepository.Add(bet);
            var result = await _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return result;
        }
        public async Task<IEnumerable<Bet>> Close(long id)
        {
            var roulette = await _unitOfWork.RouletteRepository.GetAsync(id);
            roulette.IsOpen = false;
            roulette.ClosingDate = DateTime.Now;
            var bets = await _unitOfWork.BetRepository.GetWhereAsync(x => x.RouletteId == id);
            SelectWinners(bets);
            await _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return bets;
        }
        public long Create()
        {
            var roulette = new Roulette();
            _unitOfWork.RouletteRepository.Add(roulette);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return roulette.Id;
        }
        public async Task<IEnumerable<Roulette>> GetAll()
        {
            var roulettes = await _unitOfWork.RouletteRepository.GetAllAsync();
            _unitOfWork.Dispose();
            return roulettes;
        }
        public async Task<bool> Open(long id)
        {
            var roulette = await _unitOfWork.RouletteRepository.GetAsync(id);
            if (roulette == null) return false;
            roulette.IsOpen = true;
            roulette.OpeningDate = DateTime.Now;
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
                Amount = x.Amount * WINNER_MULTIPLICATION_FACTOR,
                RouletteId = x.RouletteId,
                UserId = x.UserId
            });
            _unitOfWork.WinnerRepository.AddRange(winners);
            var colorWinnerBets = bets.Where(x => GetColor(x.Number) == GetColor(winnerNumber));
            var colorWinners = colorWinnerBets.Select(x => new Winner
            {
                Amount = x.Amount * COLOR_WINNER_MULTIPLICATION_FACTOR,
                RouletteId = x.RouletteId,
                UserId = x.UserId
            });
            _unitOfWork.WinnerRepository.AddRange(colorWinners);
        }
        private RouletteColor.Color GetColor(int number)
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
                   bet.Amount > MAX_AMOUNT_BET ||
                   !(ROULETTE_MIN_NUMBER <= bet.Number && bet.Number <= ROULETTE_MAX_NUMBER) ||
                   (bet.Number == null && bet.Color == null) ||
                   (bet.Number != null && bet.Color != null);
        }
    }
}
