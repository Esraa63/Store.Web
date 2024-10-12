using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specifications.ProductSpecs;
using Store.Service.OrderService.Dtos;
using Store.Service.Services.BasketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IBasketService basketService
            , IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
            var basket=await _basketService.GetBasketAsync(input.BasketId);
            if (basket == null)
                throw new Exception("Basket Not Exist");
            #region Fill Order Item List With Items in the Basket

            var orderItems = new List<OrderItemDto>();
            foreach (var item in basket.BascetItems)
            {
                var productItem = await _unitOfWork.Repoistory<Product, int>().GetByIdAsync(item.ProductId);
                if (productItem == null)
                    throw new Exception($"Product With Id : {item.ProductId} Not Exist");
                var itemOrdered = new ProductItem
                {
                    PictureUrl = productItem.PictureUrl,
                    ProductName = productItem.Name,
                    ProductId = item.ProductId
                };
                var orderItem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = item.Quantity,
                    ProductItem = itemOrdered
                };
                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItem);
                orderItems.Add(mappedOrderItem);
            }
                #endregion

                #region Get DeliveryMethod

                var deliveryMethod = await _unitOfWork.Repoistory<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);
                if (deliveryMethod == null)
                    throw new Exception("DeliveryMethod Not Provided");
                #endregion


                #region Caluclate Subtotal

                var subtotal = orderItems.Sum(item => item.Quantity * item.Price);
                #endregion

                #region To Do Payment

                #endregion

                #region Create Order
                var mappedShippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);
                var mappedOrderItems=_mapper.Map<List<OrderItem>>(orderItems);
                var order = new Order
                {

                    DeliveryMethodId = deliveryMethod.Id,
                    ShippingAddress = mappedShippingAddress,
                    BuyerEmail= input.BuyerEmail,
                    BasketId= input.BasketId,
                    OrderItems=mappedOrderItems,
                    SubTotal=subtotal,
                };
                await _unitOfWork.Repoistory<Order, Guid>().AddAsync(order);
                await _unitOfWork.CompleteAsync();
                var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
                return mappedOrder;
                #endregion
            
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethod()
        => await _unitOfWork.Repoistory<DeliveryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var specs = new OrderWithItemSpecification(buyerEmail);
            var orders = await _unitOfWork.Repoistory<Order, Guid>().GetAllWithSpesificationAsync(specs);
            if (!orders.Any())
                throw new Exception("You Do Not have any Order yet!");
            var mappedOrders = _mapper.Map<List<OrderDetailsDto>>(orders);
            return mappedOrders;
        }

        public async Task<OrderDetailsDto> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderWithItemSpecification(id);
            var order = await _unitOfWork.Repoistory<Order, Guid>().GetWithSpecificationByIdAsync(specs);
            if (order is null)
                throw new Exception($"There Is no Order with Id {id}");
            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }
    }
}
