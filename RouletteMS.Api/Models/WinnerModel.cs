using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Api.Models
{
    public class WinnerModel
    {
        public long Id { get; set; }
        public long RouletteId { get; set; }
        public long UserId { get; set; }
        public double Amount { get; set; }
    }
}
