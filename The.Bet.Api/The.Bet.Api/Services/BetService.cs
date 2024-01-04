using The.Bet.Api.Models;
using The.Bet.Api.Repository.Interfaces;
using The.Bet.Api.Services.Interfaces;

namespace The.Bet.Api.Services
{
    public class BetService : IBetService
    {
        private readonly IBetRepository _iBetRepository;

        public BetService(IBetRepository betRepository)
        {
            _iBetRepository = betRepository;
        }
        public async Task<string> GetBets()
        {
            return await _iBetRepository.GetBets();
        }

        public async Task<string> Payout()
        {
            return await _iBetRepository.Payout();
        }

        public async Task<int> PlaceBet(BetModel bet)
        {
           return await _iBetRepository.PlaceBet(bet);
        }

        public async Task<int> Spin()
        {
            return await _iBetRepository.Spin();
        }
    }
}
