using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Http;

namespace Agathas.Storefront.Repository.NHibernate.SessionStorage {
  public static class SessionStorageFactory {
    private static ISessionStorageContainer _nhSessionStorageContainer;

    public static ISessionStorageContainer GetStorageContainer(IHttpContextAccessor context) {
      if (_nhSessionStorageContainer == null) {
          if (context.HttpContext == null)
            _nhSessionStorageContainer = new ThreadSessionStorageContainer();
          else
            _nhSessionStorageContainer = new HttpSessionContainer(context);
      }

      return _nhSessionStorageContainer;
    }
  }
}
