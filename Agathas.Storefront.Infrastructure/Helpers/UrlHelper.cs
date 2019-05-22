using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Agathas.Storefront.Infrastructure.Helpers {
  public static class UrlHelper   {
    // 5/22/2019 - w.sams - added context, need to update what's calling this.
    public static string Resolve(IHttpContextAccessor context, string resource) {
      return string.Format("{0}://{1}{2}{3}",
          context.HttpContext.Request.Scheme,
          context.HttpContext.Request.Host,
          context.HttpContext.Request.Path.Value.Equals("/") ? string.Empty : context.HttpContext.Request.Path.Value,
          resource);
    }

    //5/21/2019 - w.sams - added this because HttpContext.Request.BinaryRead isn't a thing in .NET Core
    public static byte[] ReadRequestBody(Stream input) {
      using (MemoryStream ms = new MemoryStream())  {
        input.CopyTo(ms);
        return ms.ToArray();
      }
    }
  }
}

