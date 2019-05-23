using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Web = Microsoft.AspNetCore.Http;

using NHibernate;
using NHibernate.Cfg;

using log4net;

namespace Agathas.Storefront.Repository.NHibernate.SessionStorage {
  public class SessionFactory {
    private static ISessionFactory _sessionFactory;

    private static void Init() {
      Configuration config = new Configuration();
      config.AddAssembly("Agathas.Storefront.Repository.NHibernate");

      var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
      log4net.Config.XmlConfigurator.Configure(logRepository);

      config.Configure();

      _sessionFactory = config.BuildSessionFactory();
    }

    private static ISessionFactory GetSessionFactory() {
      if (_sessionFactory == null)
        Init();

      return _sessionFactory;
    }

    private static ISession GetNewSession() {
      return GetSessionFactory().OpenSession();
    }

    public static ISession GetCurrentSession(Web.IHttpContextAccessor context) {
      ISessionStorageContainer sessionStorageContainer =
                                  SessionStorageFactory.GetStorageContainer(context);

      ISession currentSession = sessionStorageContainer.GetCurrentSession();

      if (currentSession == null) {
        currentSession = GetNewSession();
        sessionStorageContainer.Store(currentSession);
      }

      return currentSession;
    }
  }
}
