using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Mvc;

using Agathas.Storefront.Controllers.ViewModels;
using Agathas.Storefront.Controllers.ViewModels.ProductCatalog;
using Agathas.Storefront.Infrastructure.CookieStorage;
using Agathas.Storefront.Infrastructure.Logging;
using Agathas.Storefront.Services.Implementations;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;

namespace Agathas.Storefront.API.Controllers {    
  [ApiController]
  public class BasketController : ProductCatalogBaseController {
    private readonly IBasketService _basketService;
    private readonly ICookieStorageService _cookieStorageService;

    public BasketController(IProductCatalogService productCatalogueService,
            IBasketService basketService,
            ICookieStorageService cookieStorageService) : base(cookieStorageService, productCatalogueService) {
      _basketService = basketService;
      _cookieStorageService = cookieStorageService;
    }

    [Route("api/productcatalog/basket/detail")]
    [HttpGet]
    public ActionResult<BasketDetailView> Detail() {
      var basketView = new BasketDetailView();
      var basketId = base.GetBasketId();

      var basketRequest = new GetBasketRequest() { BasketId = basketId };
      var basketResponse = _basketService.GetBasket(basketRequest);

      var dispatchOptionsResponse = _basketService.GetAllDispatchOptions();

      basketView.Basket = basketResponse.Basket;
      basketView.Categories = base.GetCategories();
      basketView.BasketSummary = base.GetBasketSummaryView();
      basketView.DeliveryOptions = dispatchOptionsResponse.DeliveryOptions;

      return basketView;
    }

    [Route("api/productcatalog/basket/removeitem")]
    [HttpPost("{productId}")]
    public BasketDetailView RemoveItem(int productId) {
      var request = new ModifyBasketRequest();
      request.ItemsToRemove.Add(productId);
      request.BasketId = base.GetBasketId();

      var reponse = _basketService.ModifyBasket(request);

      SaveBasketSummaryToCookie(reponse.Basket.NumberOfItems,
                                reponse.Basket.BasketTotal);

      var basketDetailView = new BasketDetailView();

      basketDetailView.BasketSummary = new BasketSummaryView() {
        BasketTotal = reponse.Basket.BasketTotal,
        NumberOfItems = reponse.Basket.NumberOfItems
      };

      basketDetailView.Basket = reponse.Basket;
      basketDetailView.DeliveryOptions =
              _basketService.GetAllDispatchOptions().DeliveryOptions;

      return basketDetailView;
    }

    [Route("api/productcatalog/basket/updateshipping")]
    [HttpPost("{shippingServiceId}")]
    public BasketDetailView UpdateShipping(int shippingServiceId) {
      var request = new ModifyBasketRequest();
      request.SetShippingServiceIdTo = shippingServiceId;
      request.BasketId = base.GetBasketId();

      var basketDetailView = new BasketDetailView();
      var reponse = _basketService.ModifyBasket(request);

      SaveBasketSummaryToCookie(reponse.Basket.NumberOfItems,
                                reponse.Basket.BasketTotal);

      basketDetailView.BasketSummary = new BasketSummaryView() {
        BasketTotal = reponse.Basket.BasketTotal,
        NumberOfItems = reponse.Basket.NumberOfItems
      };

      basketDetailView.Basket = reponse.Basket;
      basketDetailView.DeliveryOptions =
              _basketService.GetAllDispatchOptions().DeliveryOptions;

      return basketDetailView;
    }

    [Route("api/productcatalog/basket/update")]
    [HttpPost]
    public BasketDetailView UpdateItems(DTOs.BasketQtyUpdateRequest basketQtyUpdateRequest) {
      var request = new ModifyBasketRequest();
      request.BasketId = base.GetBasketId();
      request.ItemsToUpdate = DTOs.DtoMapper.ConvertToBasketItemUpdateRequests(
          basketQtyUpdateRequest);

      BasketDetailView basketDetailView = new BasketDetailView();
      ModifyBasketResponse reponse = _basketService.ModifyBasket(request);

      SaveBasketSummaryToCookie(reponse.Basket.NumberOfItems, reponse.Basket.BasketTotal);

      basketDetailView.BasketSummary = new BasketSummaryView() {
        BasketTotal = reponse.Basket.BasketTotal,
        NumberOfItems = reponse.Basket.NumberOfItems
      };

      basketDetailView.Basket = reponse.Basket;

      basketDetailView.DeliveryOptions = _basketService
              .GetAllDispatchOptions().DeliveryOptions;

      return basketDetailView;
    }

    [Route("api/productcatalog/basket/add")]
    [HttpPost("{productId}")]
    public BasketSummaryView AddToBasket(int productId) {
      BasketSummaryView basketSummaryView = new BasketSummaryView();
      Guid basketId = base.GetBasketId();
      bool createNewBasket = basketId == Guid.Empty;

      if (createNewBasket == false) {
        var modifyBasketRequest = new ModifyBasketRequest();
        modifyBasketRequest.ProductsToAdd.Add(productId);
        modifyBasketRequest.BasketId = basketId;

        try {
          var response = _basketService.ModifyBasket(modifyBasketRequest);
          basketSummaryView = DTOs.BasketMapper.ConvertToSummary(response.Basket);
          SaveBasketSummaryToCookie(basketSummaryView.NumberOfItems,
                                    basketSummaryView.BasketTotal);
        } catch (BasketDoesNotExistException ex) {
          LoggingFactory.GetLogger().Log(
                  String.Format("Creating new basket because exception. {0}", ex.ToString()));
          createNewBasket = true;
        }
      }

      if (createNewBasket) {
        var createBasketRequest = new CreateBasketRequest();
        createBasketRequest.ProductsToAdd.Add(productId);

        var response = _basketService.CreateBasket(createBasketRequest);

        SaveBasketIdToCookie(response.Basket.Id);
        basketSummaryView = DTOs.BasketMapper.ConvertToSummary(response.Basket);
        SaveBasketSummaryToCookie(basketSummaryView.NumberOfItems,
                                  basketSummaryView.BasketTotal);
      }

      return basketSummaryView;
    }

    private void SaveBasketIdToCookie(Guid basketId) {
      _cookieStorageService.Save(CookieDataKeys.BasketId.ToString(),
                                  basketId.ToString(), DateTime.Now.AddDays(1));
    }

    private void SaveBasketSummaryToCookie(int numberOfItems, string basketTotal) {
      _cookieStorageService.Save(CookieDataKeys.BasketItems.ToString(),
                                numberOfItems.ToString(), DateTime.Now.AddDays(1));
      _cookieStorageService.Save(CookieDataKeys.BasketTotal.ToString(),
                                  basketTotal.ToString(), DateTime.Now.AddDays(1));
    }
  }
}
