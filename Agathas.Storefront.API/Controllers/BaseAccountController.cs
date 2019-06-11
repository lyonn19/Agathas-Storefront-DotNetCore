using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Mvc;

using Agathas.Storefront.Controllers.ActionArguments;
using Agathas.Storefront.Infrastructure.Authentication;
using Agathas.Storefront.Services.Interfaces;

namespace Agathas.Storefront.API.Controllers {
  [ApiController]
  public class BaseAccountController : ControllerBase {
    protected readonly ILocalAuthenticationService _authenticationService;
    protected readonly ICustomerService _customerService;
    protected readonly IExternalAuthenticationService
                                          _externalAuthenticationService;
    protected readonly IFormsAuthentication _formsAuthentication;
    protected readonly IActionArguments _actionArguments;

    public BaseAccountController(
            ILocalAuthenticationService authenticationService,
            ICustomerService customerService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments) {
     // _authenticationService = authenticationService;
      _customerService = customerService;
      _externalAuthenticationService = externalAuthenticationService;
      _formsAuthentication = formsAuthentication;
      _actionArguments = actionArguments;
    }

    [HttpGet("{returnUrl}")]
    public ActionResult RedirectBasedOn(string returnUrl) {
      return Redirect(returnUrl);
    }
  }
}
