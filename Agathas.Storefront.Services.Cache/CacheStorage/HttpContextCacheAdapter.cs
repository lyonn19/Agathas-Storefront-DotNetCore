using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Http;

namespace Agathas.Storefront.Services.Cache.CacheStorage {
  public class HttpContextCacheAdapter : ICacheStorage {  
    private IHttpContextAccessor _context;

    public HttpContextCacheAdapter(IHttpContextAccessor context){
      _context = context;
    }      
    public void Remove(string key) {
      _context.HttpContext.Request.Headers.Remove(key);   
    }

    public void Store(string key, object data) {
      _context.HttpContext.Request.Headers.Add(key, data.ToString());    
    }

    public T Retrieve<T>(string key) {
      object itemStored = _context.HttpContext.Request.Headers[key];
      if (itemStored == null)
          itemStored = default(T);

      return (T)itemStored;       
    }       
  }
}
