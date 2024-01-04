using The.Bet.Api.Models;

namespace The.Bet.Api.Repository.Interfaces
{
    public interface IBetRepository
    {
        Task<int> PlaceBet(BetModel bet);
        Task<int> Spin();
        Task<string> GetBets();
        Task<string> Payout();
    }
}
