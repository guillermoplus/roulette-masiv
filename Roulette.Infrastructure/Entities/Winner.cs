using Roulette.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Infrastructure.Entities
{
    public class Winner : BaseEntity
    {
        public long RouletteId { get; set; }
        public long UserId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
