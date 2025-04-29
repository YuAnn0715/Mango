namespace Mango.Services.OrderAPI.Utility
{
    // 訂單相關狀態名稱
    public class SD
    {
        // 等待確認
        public const string Status_Pending = "Pending";

        // 已確認
        public const string Status_Approved = "Approved";

        // 準備中
        public const string Status_ReadyForPickup = "ReadyForPickup";

        // 已完成
        public const string Status_Completed = "Completed";

        // 退款
        public const string Status_Refunded = "Refunded";

        // 已取消
        public const string Status_Cancelled = "Cancelled";

        // 使用者:ADMIN
        public const string RoleAdmin = "ADMIN";

        // 使用者:CUSTOMER
        public const string RoleCustomer = "CUSTOMER";
    }
}