using System.Reflection;

using log4net;
using log4net.Config;

using Agathas.Storefront.Infrastructure.Configuration;

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
