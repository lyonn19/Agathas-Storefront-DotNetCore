using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Agathas.Storefront.Infrastructure.Configuration;

using log4net;
using log4net.Config;

namespace Agathas.Storefront.Infrastructure.Logging {
  public class Log4NetAdapter : ILogger {
    private readonly log4net.ILog _log;

    public Log4NetAdapter(string logName) {
      var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
      XmlConfigurator.Configure(logRepository);
      _log = LogManager
        .GetLogger(logRepository.Name, ApplicationSettingsFactory.GetApplicationSettings().LoggerName);
    }

    public void Log(string message) { _log.Info(message); }
  }
}
