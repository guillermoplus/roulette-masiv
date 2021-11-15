using Microsoft.AspNetCore.Mvc;
using RouletteMS.Api.Models;
using RouletteMS.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Api.Controllers
{
    [Route("api/[controller]")]
    public class RoulettesController : ControllerBase
    {
        private readonly IRouletteService _rouletteService;
        public RoulettesController(IRouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roulettes = await _rouletteService.GetAll();
            return Ok(roulettes);
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var rouletteId = await _rouletteService.Create();
            return Ok(rouletteId);
        }
        [HttpPatch("{id}/open")]
        public async Task<IActionResult> Open(long id)
        {
            var state = await _rouletteService.Open(id);
            return Ok(state);
        }
        [HttpPost("{id}/bet")]
        public async Task<IActionResult> Bet(BetModel model)
        {
            return Ok();
        }
        [HttpPatch("{id}/close")]
        public async Task<IActionResult> Close(long id)
        {
            var bets = await _rouletteService.Close(id);
            return Ok(bets);
        }
    }
}
