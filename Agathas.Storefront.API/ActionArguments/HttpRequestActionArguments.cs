using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Http;

namespace Agathas.Storefront.Controllers.ActionArguments {
  public class HttpRequestActionArguments : IActionArguments {
    private IHttpContextAccessor _context;

    public HttpRequestActionArguments(IHttpContextAccessor context){
      _context = context;
    }
    public string GetValueForArgument(ActionArgumentKey key) {
      Microsoft.Extensions.Primitives.StringValues queryVal;
      _context.HttpContext.Request.Query.TryGetValue(key.ToString(), out queryVal);
      return queryVal.FirstOrDefault();
    }
  }
}
