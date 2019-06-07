using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Controllers.ViewModels.Checkout;
using Agathas.Storefront.Infrastructure.Authentication;
using Agathas.Storefront.Infrastructure.CookieStorage;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Messaging.CustomerService;
using Agathas.Storefront.Services.Messaging.OrderService;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.API.Controllers { 
  [ApiController]
  public class CheckoutController : BaseController {
    private readonly ICookieStorageService _cookieStorageService;
    private readonly IBasketService _basketService;
    private readonly ICustomerService _customerService;
    private readonly IOrderService _orderService;
    private readonly IFormsAuthentication _formsAuthentication;

    public CheckoutController(ICookieStorageService cookieStorageService,
            IBasketService basketService, 
            ICustomerService customerService,
            IOrderService orderService,
            IFormsAuthentication formsAuthentication) : base(cookieStorageService) {
      _cookieStorageService = cookieStorageService;
      _basketService = basketService;
      _customerService = customerService;
      _orderService = orderService;
      _formsAuthentication = formsAuthentication;
    }

    [Authorize]
    [Route("api/checkout")]
    [HttpGet]
    public ActionResult<OrderConfirmationView> Checkout() {
      var customerRequest = new GetCustomerRequest() {
        CustomerIdentityToken = _formsAuthentication.GetAuthenticationToken(HttpContext.User)
      };

      var customerResponse = _customerService.GetCustomer(customerRequest);
      var customerView = customerResponse.Customer;


      if (customerView.DeliveryAddressBook.Count() > 0) {
        var orderConfirmationView = new OrderConfirmationView();
        var getBasketRequest = new GetBasketRequest() {
          BasketId = base.GetBasketId()
        };

        var basketResponse = _basketService.GetBasket(getBasketRequest);

        orderConfirmationView.Basket = basketResponse.Basket;
        orderConfirmationView.DeliveryAddresses = customerView.DeliveryAddressBook;

        return orderConfirmationView;
      }

      return AddDeliveryAddress().Result;
    }

    [Authorize]
    [Route("api/checkout/add")]
    [HttpGet]
    public ActionResult<DeliveryAddressView> AddDeliveryAddress() {
      return new DeliveryAddressView();
    }

    [Authorize]
    [Route("api/checkout/add")]
    [HttpPost("{deliveryAddressView}")]
    public ActionResult AddDeliveryAddress(DeliveryAddressView deliveryAddressView) {
      var request = new DeliveryAddressAddRequest();
      request.Address = deliveryAddressView;
      request.CustomerIdentityToken = _formsAuthentication.GetAuthenticationToken(HttpContext.User);

      _customerService.AddDeliveryAddress(request);

      return Checkout().Result;
    }

    [Route("api/checkout/placeorder")]
    [HttpGet]
    [Authorize]    
    public ActionResult PlaceOrder(FormCollection collection) {
      CreateOrderRequest request = new CreateOrderRequest();
      request.BasketId = base.GetBasketId();
      request.CustomerIdentityToken = 
              _formsAuthentication.GetAuthenticationToken(HttpContext.User);
      request.DeliveryId =
              int.Parse(collection[FormDataKeys.DeliveryAddress.ToString()]);

      CreateOrderResponse response = _orderService.CreateOrder(request);


      _cookieStorageService.Save(CookieDataKeys.BasketItems.ToString(),
                                  "0", DateTime.Now.AddDays(1));
      _cookieStorageService.Save(CookieDataKeys.BasketTotal.ToString(),
                                  "0", DateTime.Now.AddDays(1));

      return RedirectToAction("CreatePaymentFor", "Payment",
                              new { orderId = response.Order.Id });
    }
  }
}
