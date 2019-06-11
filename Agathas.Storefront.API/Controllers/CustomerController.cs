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
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.API.Controllers { 
  [Route("api/customer")]
  [Authorize]    
  public class CustomerController : BaseController {
    private readonly ICustomerService _customerService;
    private readonly IFormsAuthentication _formsAuthentication;

    public CustomerController(ICookieStorageService cookieStorageService,
            ICustomerService customerService,
            IFormsAuthentication formsAuthentication) : base(cookieStorageService) {
      _customerService = customerService;
      _formsAuthentication = formsAuthentication;
    }

    [Authorize]
    [HttpGet]
    public ActionResult<CustomerDetailView> Detail() {
      GetCustomerRequest customerRequest = new GetCustomerRequest();
      customerRequest.CustomerIdentityToken =
              _formsAuthentication.GetAuthenticationToken(HttpContext.User);

      GetCustomerResponse response = _customerService.GetCustomer(customerRequest);

      CustomerDetailView customerDetailView = new CustomerDetailView();
      customerDetailView.Customer = response.Customer;
      customerDetailView.BasketSummary = base.GetBasketSummaryView();

      return customerDetailView;
    }

    [Authorize]
    [HttpPost]
    public ActionResult<CustomerDetailView> Detail(CustomerView customerView) {
      ModifyCustomerRequest request = new ModifyCustomerRequest();

      request.CustomerIdentityToken = 
              _formsAuthentication.GetAuthenticationToken(HttpContext.User);
      request.Email = customerView.Email;
      request.FirstName = customerView.FirstName;
      request.SecondName = customerView.SecondName;

      ModifyCustomerResponse response = _customerService.ModifyCustomer(request);

      CustomerDetailView customerDetailView = new CustomerDetailView();

      customerDetailView.Customer = response.Customer;
      customerDetailView.BasketSummary = base.GetBasketSummaryView();

      return customerDetailView;
    }

    [Authorize]
    [HttpGet]
    public ActionResult<CustomerDetailView> DeliveryAddresses() {
      GetCustomerRequest customerRequest = new GetCustomerRequest();
      customerRequest.CustomerIdentityToken =
              _formsAuthentication.GetAuthenticationToken(HttpContext.User);

      GetCustomerResponse response = _customerService.GetCustomer(customerRequest);

      CustomerDetailView customerDetailView = new CustomerDetailView();

      customerDetailView.Customer = response.Customer;
      customerDetailView.BasketSummary = base.GetBasketSummaryView();

      return customerDetailView;
    }

    [Authorize]
    [HttpGet("{deliverAddressId}")]
    public ActionResult<CustomerDeliveryAddressView> EditDeliveryAddress(int deliveryAddressId) {
      var customerRequest = new GetCustomerRequest();
      customerRequest.CustomerIdentityToken =
                        _formsAuthentication.GetAuthenticationToken(HttpContext.User);

      var response = _customerService.GetCustomer(customerRequest);
      var deliveryAddressView = new CustomerDeliveryAddressView();

      deliveryAddressView.CustomerView = response.Customer;
      deliveryAddressView.Address = response.Customer.DeliveryAddressBook
                            .Where(d => d.Id == deliveryAddressId).FirstOrDefault();
      deliveryAddressView.BasketSummary = base.GetBasketSummaryView();

      return deliveryAddressView;
    }

    [Authorize]
    [HttpPost]
    public ActionResult EditDeliveryAddress(DeliveryAddressView deliveryAddressView) {
      var request = new DeliveryAddressModifyRequest();
      request.Address = deliveryAddressView;
      request.CustomerIdentityToken = 
              _formsAuthentication.GetAuthenticationToken(HttpContext.User);

      _customerService.ModifyDeliveryAddress(request);

      return DeliveryAddresses().Result;
    }

    [Authorize]
    [HttpPost]
    public ActionResult<CustomerDeliveryAddressView> AddDeliveryAddress() {
      var customerDeliveryAddressView = new CustomerDeliveryAddressView();

      customerDeliveryAddressView.Address = new DeliveryAddressView();
      customerDeliveryAddressView.BasketSummary = base.GetBasketSummaryView();

      return customerDeliveryAddressView;
    }

    [Authorize]
    [HttpPost]
    public ActionResult AddDeliveryAddress(DeliveryAddressView deliveryAddressView) {
      var request = new DeliveryAddressAddRequest();
      request.Address = deliveryAddressView;
      request.CustomerIdentityToken = 
              _formsAuthentication.GetAuthenticationToken(HttpContext.User);

      _customerService.AddDeliveryAddress(request);

      return DeliveryAddresses().Result;
    }
  }
}
