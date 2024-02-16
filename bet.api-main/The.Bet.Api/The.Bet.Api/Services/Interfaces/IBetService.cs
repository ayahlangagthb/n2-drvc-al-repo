using The.Bet.Api.Models;

namespace The.Bet.Api.Services.Interfaces
{
    public interface IBetService
    {
        Task<int> PlaceBet(BetModel bet);
        Task<int> Spin();
        Task<string> GetBets();
        Task<string> Payout();
    }
}
