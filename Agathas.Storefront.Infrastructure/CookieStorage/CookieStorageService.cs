using System;

using Microsoft.AspNetCore.Http;

namespace Agathas.Storefront.Infrastructure.CookieStorage {
  public class CookieStorageService : ICookieStorageService
  {
    private readonly IHttpContextAccessor _context;
    
    // 5/22/2019 - w.sams - added context, need to update what's calling this.
    public CookieStorageService(IHttpContextAccessor context) {
      this._context = context;
    }
    
    public void Save(string key, string value, DateTime expires) {
      //_context.HttpContext.Response.Cookies.Cookies[key].Value = value;
      // _context.HttpContext.Response.Cookies[key].Expires = expires;
      this._context.HttpContext.Response.Cookies.Append(
        key, value, new CookieOptions() { Expires = expires });
    }

    public string Retrieve(string key) {
      //HttpCookie cookie = _context.HttpContext.Request.Cookies[key];
      var cookie = this._context.HttpContext.Request.Cookies[key];
      if (cookie != String.Empty) // != null)
          return cookie; //cookie.Value;
      return "";
    }
  }
}
