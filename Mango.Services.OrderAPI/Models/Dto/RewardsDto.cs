namespace Mango.Services.OrderAPI.Models.Dto
{
    public class RewardsDto
    {
        // 使用者Id
        public string UserId { get; set; }

        // 獎勵積分數
        public int RewardsActivity { get; set; }

        // 訂單Id
        public int OrderId { get; set; }
    }
}
