using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using The.Bet.Api.Models;
using The.Bet.Api.Services.Interfaces;

namespace The.Bet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {

        private readonly IBetService _betService;
        public BetController(IBetService betService)
        {
            _betService = betService;
        }
        [HttpGet]
        [Route("GetAllPreviousBets")]
        public async Task<IActionResult> GetAllBets()
        {
            var res = await _betService.GetBets();
            return Ok(res);
        }

        [HttpPost]
        [Route("PlaceBet")]
        public async Task<IActionResult> PlaceBet(BetModel bet)
        {
            var res = await _betService.PlaceBet(bet);
            return Ok(res);
        }

        [HttpPost]
        [Route("Spin")]
        public async Task<IActionResult> Spin()
        {
            var res = await _betService.Spin();
            return Ok(res);
        }
        [HttpGet]
        [Route("GetPayout")]
        public async Task<IActionResult> Payout()
        {
            var res = await _betService.Payout();
            return Ok(res);
        }

    }
}
