using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// 訂單管理頁面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult OrderIndex()
        {
            return View();
        }

        /// <summary>
        /// 訂單細節
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> OrderDetail(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
            string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            var response = await _orderService.GetOrder(orderId);
            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            }
            if (!User.IsInRole(SD.RoleAdmin) && userId != orderHeaderDto.UserId)
            {
                return NotFound();
            }
            return View(orderHeaderDto);
        }

        /// <summary>
        /// 準備好訂單
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost("OrderReadyForPickup")]
        public async Task<IActionResult> OrderReadyForPickup(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_ReadyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

        /// <summary>
        /// 完成訂單
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Completed);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

        /// <summary>
        /// 取消訂單
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Cancelled);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

        /// <summary>
        /// 取得指定狀態的全部訂單
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = "";
            // 檢查是否為管理員
            if (!User.IsInRole(SD.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            }
            ResponseDto response = _orderService.GetAllOrder(userId).GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
                switch (status)
                {
                    case "approved":
                        list = list.Where(u => u.Status == SD.Status_Approved);
                        break;
                    case "readyforpickup":
                        list = list.Where(u => u.Status == SD.Status_ReadyForPickup);
                        break;
                    case "cancelled":
                        list = list.Where(u => u.Status == SD.Status_Cancelled || u.Status == SD.Status_Refunded);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                list = new List<OrderHeaderDto>();
            }
            return Json(new { data = list.OrderByDescending(u => u.OrderHeaderId) });
        }
    }
}