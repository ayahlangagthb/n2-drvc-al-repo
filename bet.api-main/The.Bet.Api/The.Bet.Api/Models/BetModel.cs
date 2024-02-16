namespace The.Bet.Api.Models
{
    public class BetModel
    {
        public string Player { get; set; }
        public int BetNumber { get; set; }
        public decimal Amount { get; set; }
        public string BetType { get; set; }
    }
}
