using RouletteMS.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Infrastructure.Entities
{
    public class Winner : BaseEntity<long>
    {
        public long RouletteId { get; set; }
        public long UserId { get; set; }
        public double Amount { get; set; }
    }
}
