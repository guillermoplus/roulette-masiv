﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Domain.Dtos
{
    public class RouletteDto
    {
        public long Id { get; set; }
        public bool IsOpen { get; set; }
        public double? MaxAmountToBet { get; set; }
        public double? TotalAmountBet { get; set; }
        public double? TotalAmountDelivered { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
    }
}
