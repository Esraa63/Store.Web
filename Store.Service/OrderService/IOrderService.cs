using Store.Data.Entities;
using Store.Service.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService
{
    public interface IOrderService
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderDto orderDto);
        Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string buyerEmail);
        Task<OrderDetailsDto> GetOrderByIdAsync(Guid id);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethod();
    }
}
