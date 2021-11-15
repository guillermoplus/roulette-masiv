using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Infrastructure.Entities
{
    public class Roulette : BaseEntity<long>
    {
        public bool IsOpen { get; set; }
        public double? MaxAmountToBet { get; set; } = null;
        public double? TotalAmountBet { get; set; } = null;
        public double? TotalAmountDelivered { get; set; } = null;
        public DateTime? OpeningDate { get; set; } = null;
        public DateTime? ClosingDate { get; set; } = null;
    }
}
