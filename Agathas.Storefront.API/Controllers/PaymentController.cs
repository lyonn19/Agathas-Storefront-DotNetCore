using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Logging;
using Agathas.Storefront.Infrastructure.Payments;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Messaging.OrderService;

namespace Agathas.Storefront.API.Controllers {
  [ApiController]
  public class PaymentController : ControllerBase {
    private readonly IPaymentService _paymentService;
    private readonly IOrderService _orderService;

    public PaymentController(IPaymentService paymentService,
                              IOrderService orderService) {
      _paymentService = paymentService;
      _orderService = orderService;
    }
    
    [Route("api/payment")]
    [HttpPost]
    public void PaymentCallBack(FormCollection collection) {
      int orderId = _paymentService.GetOrderIdFor(collection);
      var request = new GetOrderRequest() { OrderId = orderId };

      var response = _orderService.GetOrder(request);
      OrderPaymentRequest orderPaymentRequest =
            DTOs.OrderMapper.ConvertToOrderPaymentRequest(response.Order);

      var transactionResult =
              _paymentService.HandleCallBack(orderPaymentRequest, collection);

      if (transactionResult.PaymentOk) {
        var paymentRequest = new SetOrderPaymentRequest();
        paymentRequest.Amount = transactionResult.Amount;
        paymentRequest.PaymentToken = transactionResult.PaymentToken;
        paymentRequest.PaymentMerchant = transactionResult.PaymentMerchant;
        paymentRequest.OrderId = orderId;

        _orderService.SetOrderPayment(paymentRequest);
      } else {
        LoggingFactory.GetLogger().Log(String.Format(
                "Payment not ok for order id {0}, payment token {1}",
                orderId, transactionResult.PaymentToken));
      }
    }

    [Route("api/payment/create")]
    [HttpGet("{orderId)}")]
    public ActionResult<PaymentPostData> CreatePaymentFor(int orderId) {
      var request = new GetOrderRequest() { OrderId = orderId };

      GetOrderResponse response = _orderService.GetOrder(request);
      OrderPaymentRequest orderPaymentRequest =
              DTOs.OrderMapper.ConvertToOrderPaymentRequest(response.Order);

      PaymentPostData paymentPostData =
              _paymentService.GeneratePostDataFor(orderPaymentRequest);

      return paymentPostData;
    }

    [Route("api/payment/complete")]
    [HttpGet]public ActionResult<string> PaymentComplete() { return "Payment complete."; }

    [Route("api/payment/cancel")]
    [HttpGet]public ActionResult<string> PaymentCancel() { return "Payment cancelled."; }
  }
}
