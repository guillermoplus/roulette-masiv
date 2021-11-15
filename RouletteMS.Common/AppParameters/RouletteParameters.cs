using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Common.AppParameters
{
    public static class RouletteParameters
    {
        public static readonly double MAX_AMOUNT_BET = double.Parse(Environment.GetEnvironmentVariable("MAX_AMOUNT_BET"));
        public static readonly double WINNER_MULTIPLICATION_FACTOR = double.Parse(Environment.GetEnvironmentVariable("WINNER_MULTIPLICATION_FACTOR"));
        public static readonly double COLOR_WINNER_MULTIPLICATION_FACTOR = double.Parse(Environment.GetEnvironmentVariable("COLOR_WINNER_MULTIPLICATION_FACTOR"));
        public static readonly double ROULETTE_MAX_NUMBER = double.Parse(Environment.GetEnvironmentVariable("ROULETTE_MAX_NUMBER"));
        public static readonly double ROULETTE_MIN_NUMBER = double.Parse(Environment.GetEnvironmentVariable("ROULETTE_MIN_NUMBER"));
    }
}
