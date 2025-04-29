namespace Mango.Web.Utility
{
    public class SD
    {
        public static string CouponAPIBase { get; set; }

        public static string ProductAPIBase { get; set; }

        public static string AuthAPIBase { get; set; }

        public static string ShoppingCartAPIBase { get; set; }

        public static string OrderAPIBase { get; set; }

        // 預設常量
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookies = "JWTToken";

        // API 
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        // 訂單狀態
        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_ReadyForPickup = "ReadyForPickup";
        public const string Status_Completed = "Completed";
        public const string Status_Refunded = "Refunded";
        public const string Status_Cancelled = "Cancelled";

        // 傳遞內容資料類型
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
