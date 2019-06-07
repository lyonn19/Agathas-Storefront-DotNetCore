using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Controllers.ActionArguments;
using Agathas.Storefront.Controllers.ViewModels.Account;
using Agathas.Storefront.Infrastructure.Authentication;
using Agathas.Storefront.Services.Implementations;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Messaging.CustomerService;

namespace Agathas.Storefront.API.Controllers {
    public class AccountRegisterController : BaseAccountController {
      public AccountRegisterController(
              ILocalAuthenticationService authenticationService,
              ICustomerService customerService,
              IExternalAuthenticationService
                                  externalAuthenticationService,
              IFormsAuthentication formsAuthentication,
              IActionArguments actionArguments) : base(
                                                      authenticationService, customerService,
                                                      externalAuthenticationService,
                                                      formsAuthentication, actionArguments) {}

    [Route("api/account/register")]
    [HttpGet]
    public ActionResult<AccountView> Register() {
      AccountView accountView = InitializeAccountViewWithIssue(false, string.Empty);
      return accountView;
    }

    [Route("api/account/register")]
    [HttpPost]
    public ActionResult<AccountView> Register(FormCollection collection) {
      //note: w.sams - try serializing in JS the formcollection first before posting.
      //    i.e., $.post("/api/account/register", $("#myForm").serialize(), function(msg) {..} });
      User user;

      string password = collection[FormDataKeys.Password.ToString()];
      string email = collection[FormDataKeys.Email.ToString()];
      string firstName = collection[FormDataKeys.FirstName.ToString()];
      string secondName = collection[FormDataKeys.SecondName.ToString()];

      try {
        user = _authenticationService.RegisterUser(email, password);
      } catch (InvalidOperationException ex) {
        AccountView accountView = InitializeAccountViewWithIssue(true, ex.Message);
        return (accountView);
      }

      if (user.IsAuthenticated) {
        try {
          var createCustomerRequest = new CreateCustomerRequest();
          createCustomerRequest.CustomerIdentityToken = user.AuthenticationToken;
          createCustomerRequest.Email = email;
          createCustomerRequest.FirstName = firstName;
          createCustomerRequest.SecondName = secondName;

          _formsAuthentication.SetAuthenticationToken(user.AuthenticationToken);
          _customerService.CreateCustomer(createCustomerRequest);

          return RedirectToAction("Detail", "Customer");
        }
        catch (CustomerInvalidException ex) {
          AccountView accountView = InitializeAccountViewWithIssue( true, ex.Message);
          return accountView;
        }
      } else {
        AccountView accountView = InitializeAccountViewWithIssue(true,
                "Sorry we could not authenticate you. " +
                " Please try again.");
        return accountView;
      }
    }

    [Route("api/account/receivetokenandregister")]
    [HttpGet("{token}/{returnUrl}")]
    public ActionResult<AccountView> ReceiveTokenAndRegister(string token, string returnUrl) {
      User user = _externalAuthenticationService.GetUserDetailsFrom(token);

      if (user.IsAuthenticated) {
        _formsAuthentication.SetAuthenticationToken(user.AuthenticationToken);

        // Register user
        var createCustomerRequest =  new CreateCustomerRequest();
        createCustomerRequest.CustomerIdentityToken = user.AuthenticationToken;
        createCustomerRequest.Email = user.Email;
        createCustomerRequest.FirstName = "[Please Enter]";
        createCustomerRequest.SecondName = "[Please Enter]";

        _customerService.CreateCustomer(createCustomerRequest);

        return RedirectBasedOn(returnUrl);
      } else {
        AccountView accountView = InitializeAccountViewWithIssue(true,
                "Sorry we could not authenticate you.");
        accountView.CallBackSettings.ReturnUrl = GetReturnActionFrom(returnUrl).ToString();

        return accountView;
      }
    }

    private AccountView InitializeAccountViewWithIssue(bool hasIssue, string message) {
      AccountView accountView = new AccountView();
      accountView.CallBackSettings.Action = "ReceiveTokenAndRegister";
      accountView.CallBackSettings.Controller = "AccountRegister";
      accountView.HasIssue = hasIssue;
      accountView.Message = message;

      string returnUrl = _actionArguments.GetValueForArgument(ActionArgumentKey.ReturnUrl);
      accountView.CallBackSettings.ReturnUrl = GetReturnActionFrom(returnUrl).ToString();

      return accountView;
    }
  }
}
