using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Logging;
using Agathas.Storefront.Models.Basket;
using Agathas.Storefront.Models.Customers;
using Agathas.Storefront.Models.Orders;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Mapping;
using Agathas.Storefront.Services.Messaging.OrderService;

namespace Agathas.Storefront.Services.Implementations {
  public class OrderService : IOrderService {
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository,
                        IBasketRepository basketRepository,
                        ICustomerRepository customerRepository,
                        IUnitOfWork uow, IMapper mapper) {
      _customerRepository = customerRepository;
      _orderRepository = orderRepository;
      _basketRepository = basketRepository;
      _uow = uow;
      _mapper = mapper;
    }

    public CreateOrderResponse CreateOrder(CreateOrderRequest request) {
      var response = new CreateOrderResponse();
      var customer = _customerRepository.FindBy(request.CustomerIdentityToken);
      var basket = _basketRepository.FindBy(request.BasketId);
      var deliveryAddress = customer.DeliveryAddressBook
                    .Where(d => d.Id == request.DeliveryId).FirstOrDefault();
      var order = basket.ConvertToOrder();

      order.Customer = customer;
      order.DeliveryAddress = deliveryAddress;

      _orderRepository.Save(order);
      _basketRepository.Remove(basket);
      _uow.Commit();

      response.Order = order.ConvertToOrderView(_mapper);
      return response;
    }

    public SetOrderPaymentResponse SetOrderPayment(SetOrderPaymentRequest paymentRequest) {
      var paymentResponse = new SetOrderPaymentResponse();
      var order = _orderRepository.FindBy(paymentRequest.OrderId);

      try {
        order.SetPayment(PaymentFactory.CreatePayment(
                  paymentRequest.PaymentToken, paymentRequest.Amount, paymentRequest.PaymentMerchant));

        _orderRepository.Save(order);
        _uow.Commit();
      } catch (OrderAlreadyPaidForException ex) {
        // Out of scope of case study:  Refund the payment using the payment service.
        LoggingFactory.GetLogger().Log(ex.Message);
      } catch (PaymentAmountDoesNotEqualOrderTotalException ex) {
        // Out of scope of case study: Refund the payment using the payment service.
        LoggingFactory.GetLogger().Log(ex.Message);
      }

      paymentResponse.Order = order.ConvertToOrderView(_mapper);
      return paymentResponse;
    }

    public GetOrderResponse GetOrder(GetOrderRequest request) {
      var response = new GetOrderResponse();
      var order = _orderRepository.FindBy(request.OrderId);

      response.Order = order.ConvertToOrderView(_mapper);
      return response;
    }
  }
}
