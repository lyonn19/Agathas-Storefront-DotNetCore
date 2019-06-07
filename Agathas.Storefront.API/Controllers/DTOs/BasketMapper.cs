using Agathas.Storefront.Controllers.ViewModels;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.API.Controllers.DTOs {
  public static class BasketMapper {
    public static BasketSummaryView ConvertToSummary(BasketView basket) {
      return new BasketSummaryView() {
        BasketTotal = basket.BasketTotal,
        NumberOfItems = basket.NumberOfItems
      };
    }
  }
}
