using System;

namespace Agathas.Storefront.Controllers.ActionArguments {  
  public class ActionArguments { 
    public static ActionArgumentKey GetReturnActionFrom(string returnUrl) {
      if (!String.IsNullOrEmpty(returnUrl) &&
              returnUrl.ToLower().Contains("checkout"))
        return ActionArgumentKey.GoToCheckout;
      else return ActionArgumentKey.GoToAccount;
    }
  }
}