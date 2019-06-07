using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Agathas.Storefront.Controllers.ViewModels.CustomerAccount;
using Agathas.Storefront.Infrastructure.Authentication;
using Agathas.Storefront.Infrastructure.CookieStorage;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Messaging.CustomerService;
using Agathas.Storefront.Services.Messaging.OrderService;

namespace Agathas.Storefront.API.Controllers {
  [ApiController]    
  public class OrderController : BaseController {
    private readonly ICustomerService _customerService;
    private readonly IOrderService _orderService;
    private readonly IFormsAuthentication _formsAuthentication;

    public OrderController(ICustomerService customerService,
            IOrderService orderService,
            IFormsAuthentication formsAuthentication,
            ICookieStorageService cookieStorageService) : base(cookieStorageService) {
      _customerService = customerService;
      _orderService = orderService;
      _formsAuthentication = formsAuthentication;
    }

    [Authorize]
    [Route("api/order")]
    [HttpGet]
    public ActionResult<CustomersOrderSummaryView> List() {
      var request = new GetCustomerRequest() {
        CustomerIdentityToken = 
                _formsAuthentication.GetAuthenticationToken(HttpContext.User),
        LoadOrderSummary = true
      };

      var response = _customerService.GetCustomer(request);

      var customersOrderSummaryView = new CustomersOrderSummaryView();
      customersOrderSummaryView.Orders = response.Orders;
      customersOrderSummaryView.BasketSummary = base.GetBasketSummaryView();

      return customersOrderSummaryView;
    }

    [Authorize]
    [Route("api/order")]
    [HttpGet("{orderId}")]
    public ActionResult<CustomerOrderView> Detail(int orderId)  {
      var request = new GetOrderRequest() { OrderId = orderId };
      var response = _orderService.GetOrder(request);

      var orderView = new CustomerOrderView();
      orderView.BasketSummary = base.GetBasketSummaryView();
      orderView.Order = response.Order;

      return orderView;
    }
  }
}
