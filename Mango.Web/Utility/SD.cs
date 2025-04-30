namespace Mango.Web.Utility
{
    public class SD
    {
        // API 類別
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        // 優惠券 API
        public static string CouponAPIBase { get; set; }

        // 產品 API
        public static string ProductAPIBase { get; set; }

        // 驗證授權 API
        public static string AuthAPIBase { get; set; }

        // 購物車 API
        public static string ShoppingCartAPIBase { get; set; }

        // 訂單 API
        public static string OrderAPIBase { get; set; }

        /// <summary>
        /// 預設常量
        /// </summary>
        // 角色
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        // JWTToken
        public const string TokenCookies = "JWTToken";
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
