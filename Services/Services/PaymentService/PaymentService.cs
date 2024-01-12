using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.Extensions.Configuration;
using Services.Services.BasketService;
using Services.Services.BasketService.Dto;
using Services.Services.OrderService.Dto;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Core.Entities.Product;

namespace Services.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PaymentService(
            IUnitOfWork unitOfWork,
            IBasketService basketService,
            IConfiguration configuration,
            IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._basketService = basketService;
            this._configuration = configuration;
            this._mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];

            var basket = await _basketService.GetBasketAsync(basketId);

            if(basket == null)
                 return null;

            var shippingPrice = 0m;

            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in basket.BasketItems)
            {
                var productitem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if(item.Price != productitem.Price)
                    item.Price = productitem.Price;
            }

            var service = new PaymentIntentService();
            PaymentIntent intent;

            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + ((long)shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> {"card"}
                };

                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
                
            }
            else
            {

                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + ((long)shippingPrice * 100),
                    
                };
                await service.UpdateAsync(basket.PaymentIntentId,options);

            }

            await _basketService.UpdateBasketAsync(basket);

            return basket;

        }

        public async Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecification(paymentIntentId);

            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecificationsAsync(specs);

            if (order is null)
                return null;

            order.OrderStatus = OrderStatus.PaymentFailed;

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Complete();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);


            return mappedOrder;

        }

        public async Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecification(paymentIntentId);

            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecificationsAsync(specs);

            if (order is null)
                return null;

            order.OrderStatus = OrderStatus.PaymentReceived;

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Complete();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);


            return mappedOrder;
        }
    }
}
