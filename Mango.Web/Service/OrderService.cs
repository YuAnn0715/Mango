using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// 建立訂單
        /// </summary>
        /// <param name="userId">使用者ID</param>
        /// <returns></returns>

        public async Task<ResponseDto?> CreateOrder(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.OrderAPIBase + "/api/order/CreateOrder"
            });
        }

        /// <summary>
        ///  建立 Stripe 支付 Session
        /// </summary>
        /// <param name="stripeRequestDto">Stripe請求</param>
        /// <returns></returns>

        public async Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = stripeRequestDto,
                Url = SD.OrderAPIBase + "/api/order/CreateStripeSession"
            });
        }

        /// <summary>
        ///  檢查訂單
        /// </summary>
        /// <param name="orderHeaderId">訂單Id</param>
        /// <returns></returns>

        public async Task<ResponseDto?> ValidateStripeSession(int orderHeaderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = orderHeaderId,
                Url = SD.OrderAPIBase + "/api/order/ValidateStripeSession"
            });
        }

        /// <summary>
        /// 取得全部訂單
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> GetAllOrder(string? userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetOrders?userId=" + userId
            });
        }

        /// <summary>
        /// 取得指定訂單
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> GetOrder(int orderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetOrder/" + orderId
            });
        }

        /// <summary>
        /// 更新訂單狀態
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public async Task<ResponseDto?> UpdateOrderStatus(int orderId,string newStatus)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = newStatus,
                Url = SD.OrderAPIBase + "/api/order/UpdateOrderStatus/"+orderId
            });
        }
    }
}
