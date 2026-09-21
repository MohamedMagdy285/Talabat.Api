using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tala.Core;
using Tala.Core.Entities.Order_Aggregate;
using Tala.Core.Repositories;
using Tala.Core.Services;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Tala.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork
            //IGenericRepository<Product> productRepo,
            //IGenericRepository<DeliveryMethod> deliveryMethodRepo,
            //IGenericRepository<Order> orderRepo
            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            //_productRepo = productRepo;
            //_deliveryMethodRepo = deliveryMethodRepo;
            //_orderRepo = orderRepo;
        }


        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1. Get Basket from Basekt Repo

            var basket = await _basketRepository.GetBasketAsync(basketId);


            // 2. Get Selected Items at Basket From Products Repo

            var orderItems = new List<OrderItem>();
            if(basket?.Items.Count > 0)
            {
                foreach(var item in basket.Items)
                {
                    var productRepo = _unitOfWork.Repository<Product>();

                    if(productRepo is not null)
                    {
                        var product = await productRepo.GetByIdAsync(item.Id);
                        if (product != null)
                        {
                            var itemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                            var orderItem = new OrderItem(itemOrdered, product.Price, item.Quantity);
                            orderItems.Add(orderItem);
                        }
                    }
                       
                }
            }

            // 3. Calculate SubTotal

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4. Get DeliveryMethod From DeliveryMethods Repo

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            
            // 5. Create Order

            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal);
            await _unitOfWork.Repository<Order>().Add(order);
            
            // 6. Save to Database [ToDo]

            var result = await _unitOfWork.Complete();
            if(result > 0)
            return order;

            return null;
        }

        public Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
