using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Api.Models
{
    public class Roulette
    {
        public long Id { get; set; }
        public bool IsOpen { get; set; }
        public double MaxAmountToBet { get; set; }
        public double TotalAmountBet { get; set; }
        public double TotalAmountDelivered { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
