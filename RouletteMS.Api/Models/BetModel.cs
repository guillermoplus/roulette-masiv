﻿using RouletteMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Api.Models
{
    public class BetModel
    {
        public long Id { get; set; }
        public long RouletteId { get; set; }
        public double Amount { get; set; }
        public RouletteColor.Color Color { get; set; }
        public int Number { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
