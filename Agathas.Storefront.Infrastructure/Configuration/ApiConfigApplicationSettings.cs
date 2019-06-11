using System.Configuration;
 
namespace Agathas.Storefront.Infrastructure.Configuration {
  public class ApiConfigApplicationSettings : IApplicationSettings {
    public string LoggerName {
      get { return ConfigurationManager.AppSettings["LoggerName"]; }
    }

    public string NumberOfResultsPerPage {
      get { return ConfigurationManager.AppSettings["NumberOfResultsPerPage"]; }
    }

    public string JanrainApiKey {
      get { return ConfigurationManager.AppSettings["JanrainApiKey"]; }
    }

    public string PayPalBusinessEmail {
      get { return ConfigurationManager.AppSettings["PayPalBusinessEmail"]; }
    }

    public string PayPalPaymentPostToUrl {
      get { return ConfigurationManager.AppSettings["PayPalPaymentPostToUrl"]; }
    }
  }
}
