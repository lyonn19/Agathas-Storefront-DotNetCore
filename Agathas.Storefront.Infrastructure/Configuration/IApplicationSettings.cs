namespace Agathas.Storefront.Infrastructure.Configuration {
  public interface IApplicationSettings {
    string LoggerName { get; }
    string NumberOfResultsPerPage { get; }
    string JanrainApiKey { get;  }
    string PayPalBusinessEmail { get; }
    string PayPalPaymentPostToUrl { get; }
  }
}
