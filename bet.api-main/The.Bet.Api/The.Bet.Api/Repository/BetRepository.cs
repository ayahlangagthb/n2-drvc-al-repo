using System.Data.SqlClient;
using The.Bet.Api.Models;
using The.Bet.Api.Repository.Interfaces;
using Dapper;
using Newtonsoft.Json;

namespace The.Bet.Api.Repository
{
    public class BetRepository : IBetRepository
    {
        private readonly IConfiguration configuration;
        public BetRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> GetBets()
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("Conn")))
            {
                var obj = await connection.QueryAsync("pr_ShowPreviousSpins", commandType: System.Data.CommandType.StoredProcedure);
                string res = JsonConvert.SerializeObject(obj);
                return res;
            }
        }

        public async Task<int> PlaceBet(BetModel bet)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("Conn")))
            {
                var res = await connection.ExecuteAsync("pr_PlaceBet", bet, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        public async Task<int> Spin()
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("Conn")))
            {
                int WinningNumber = await SpinRoulette();
                DynamicParameters param = new DynamicParameters();
                param.Add("WinningNumber", WinningNumber);
                var res = await connection.ExecuteAsync("pr_SpinRoulette", param, commandType: System.Data.CommandType.StoredProcedure);
                return res = WinningNumber;
            }
        }

        static async Task<int> SpinRoulette()
        {
            int maxAttempts = 10;
            int delayMilliseconds = 500;
            int currentAttempt = 0;

            while (currentAttempt < maxAttempts)
            {
                int result = await TryToGetResultAsync();

                if (result != -1) // Replace -1 with the actual value that indicates the condition is not met
                {
                    return result;
                }
                await Task.Delay(delayMilliseconds);
                currentAttempt++;
            }

            throw new TimeoutException("Condition not met within the specified number of attempts.");
        }

        static async Task<int> TryToGetResultAsync()
        {
            // Replace this with your actual asynchronous operation
            // For example, querying a database, making an API call, etc.
            await Task.Delay(100); // Simulating an asynchronous operation
            return DateTime.Now.Second % 2 == 0 ? DateTime.Now.Second : -1;
        }

        public async Task<string> Payout()
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("Conn")))
            {
                var obj = await connection.QueryAsync("pr_Payout", commandType: System.Data.CommandType.StoredProcedure);
                string res = JsonConvert.SerializeObject(obj);
                return res;
            }
        }
    }
}

