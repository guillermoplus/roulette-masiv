using RouletteMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Infrastructure.Entities
{
    public class Bet : BaseEntity<long>
    {
        [Required]
        public long RouletteId { get; set; }
        [Required]
        public double Amount { get; set; }
        public RouletteColor.Color? Color { get; set; } = null;
        public int? Number { get; set; } = null;
        [Required]
        public long UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
