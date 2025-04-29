namespace Mango.Services.RewardAPI.Models
{
    public class Rewards
    {
        // key
        public int Id { get; set; }

        // 使用者Id
        public string UserId { get; set; }

        // 獎勵日
        public DateTime RewardsDate { get; set; }

        // 獎勵積分數
        public int RewardsActivity { get; set; }

        // 訂單Id
        public int OrderId { get; set; }
    }
}
