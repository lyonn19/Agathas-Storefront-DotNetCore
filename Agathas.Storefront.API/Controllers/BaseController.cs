using System;

using Microsoft.AspNetCore.Mvc;

using Agathas.Storefront.Controllers.ViewModels;
using Agathas.Storefront.Infrastructure.CookieStorage;

namespace Agathas.Storefront.API.Controllers{
  public class BaseController : ControllerBase {
    private readonly ICookieStorageService _cookieStorageService;

    public BaseController(ICookieStorageService cookieStorageService) {
      _cookieStorageService = cookieStorageService;
    }

    [Route("api/{controller}/baskets")]
    [HttpGet]
    public BasketSummaryView GetBasketSummaryView() {
      string basketTotal = "";
      int numberOfItems = 0;

      if (!string.IsNullOrEmpty(_cookieStorageService.Retrieve(
                                    CookieDataKeys.BasketTotal.ToString())))
        basketTotal = _cookieStorageService.Retrieve(
                                    CookieDataKeys.BasketTotal.ToString());

      if (!string.IsNullOrEmpty(_cookieStorageService.Retrieve(
                                    CookieDataKeys.BasketItems.ToString())))
        numberOfItems = int.Parse(_cookieStorageService.Retrieve(
                                    CookieDataKeys.BasketItems.ToString()));

      return new BasketSummaryView {
        BasketTotal = basketTotal,
        NumberOfItems = numberOfItems
      };
    }

    [Route("api/{controller}/basket")]
    [HttpGet]
    public Guid GetBasketId() {
      string sBasketId = _cookieStorageService.Retrieve(CookieDataKeys.BasketId.ToString());
      Guid basketId = Guid.Empty;

      if (!string.IsNullOrEmpty(sBasketId)) {
        basketId = new Guid(sBasketId);
      }

      return basketId;
    }
  }
}
