using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;

namespace Agathas.Storefront.Infrastructure.Authentication {
  public class AspFormsAuthentication : IFormsAuthentication {
    public void SetAuthenticationToken(string token) {
      //TODO:  5/22/2019 - w.sams - re-implement authentication
      //FormsAuthentication.SetAuthCookie(token, false);
    }

    //TODO:  5/22/2019 - w.sams - Modify whatever calls this function, need to pass ClaimsPrincipal
    public string GetAuthenticationToken(ClaimsPrincipal user) {
      return user.Identity.Name;
    }

    public void SignOut() {
      //TODO:  5/22/2019 - w.sams - re-implement authentication
      // FormsAuthentication.SignOut();
    }
  }
}
