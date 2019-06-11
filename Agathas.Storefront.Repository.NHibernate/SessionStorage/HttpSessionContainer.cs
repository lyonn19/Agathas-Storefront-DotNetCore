using Web = Microsoft.AspNetCore.Http;
using NHibernate;

namespace Agathas.Storefront.Repository.NHibernate.SessionStorage {
  public class HttpSessionContainer : ISessionStorageContainer {
    private string _sessionKey = "NHSession";
    private readonly Web.IHttpContextAccessor _context;
    public HttpSessionContainer(Web.IHttpContextAccessor context) {
      this._context = context;
    }

    public ISession GetCurrentSession() {
      ISession nhSession = null;

      if (this._context.HttpContext.Items.ContainsKey(_sessionKey))
          nhSession = (ISession)this._context.HttpContext.Items[_sessionKey];

      return nhSession;
    }

    public void Store(ISession session) {
      if (this._context.HttpContext.Items.ContainsKey(_sessionKey))
        this._context.HttpContext.Items[_sessionKey] = session;
      else
        this._context.HttpContext.Items.Add(_sessionKey, session);
    }
  }
}
