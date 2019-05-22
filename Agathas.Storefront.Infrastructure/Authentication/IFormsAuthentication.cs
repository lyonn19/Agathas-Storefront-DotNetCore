using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;

namespace Agathas.Storefront.Infrastructure.Authentication {
  public interface IFormsAuthentication {
    void SetAuthenticationToken(string token);

    //TODO:  5/22/2019 - w.sams - Modify whatever calls this function, need to pass ClaimsPrincipal
    string GetAuthenticationToken(ClaimsPrincipal user);
    void SignOut();
  }                

}
