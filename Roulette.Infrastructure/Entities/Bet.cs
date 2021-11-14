using RouletteMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Infrastructure.Entities
{
    public class Bet : BaseEntity<long>
    {
        public long RouletteId { get; set; }
        public double Amount { get; set; }
        public RouletteColor.Color Color { get; set; }
        public int Number { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
