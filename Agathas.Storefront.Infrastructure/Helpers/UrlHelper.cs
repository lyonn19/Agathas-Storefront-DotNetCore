using System.IO;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Agathas.Storefront.Infrastructure.Helpers {
  public static class UrlHelper   {
    public static string Resolve(IHttpContextAccessor context, string resource) {
      return string.Format("{0}://{1}{2}{3}",
          context.HttpContext.Request.Scheme,
          context.HttpContext.Request.Host,
          context.HttpContext.Request.Path.Value.Equals("/") 
                  ? string.Empty : context.HttpContext.Request.Path.Value,
          resource);
    }

    public static byte[] ReadRequestBody(Stream input) {
      using (MemoryStream ms = new MemoryStream())  {
        input.CopyTo(ms);
        return ms.ToArray();
      }
    }
  }
}

