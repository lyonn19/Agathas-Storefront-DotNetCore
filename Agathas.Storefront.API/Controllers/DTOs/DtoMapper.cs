using System.Collections.Generic;

using Agathas.Storefront.Services.Messaging.ProductCatalogService;

namespace Agathas.Storefront.API.Controllers.DTOs {
  public static class DtoMapper {
    public static IList<ProductQtyUpdateRequest> ConvertToBasketItemUpdateRequests(
              BasketQtyUpdateRequest basketQtyUpdateRequest) {
      return ConvertToBasketItemUpdateRequests(basketQtyUpdateRequest.Items);
    }

    public static IList<ProductQtyUpdateRequest> ConvertToBasketItemUpdateRequests(
            BasketItemUpdateRequest[] basketItemUpdateRequests) {
      var items = new List<ProductQtyUpdateRequest>();

      foreach(var item in basketItemUpdateRequests){
        items.Add(ConvertToBasketItemUpdateRequest(item));
      }

      return items;
    }

    public static ProductQtyUpdateRequest ConvertToBasketItemUpdateRequest(
              BasketItemUpdateRequest BasketItemUpdateRequest) {
      return new ProductQtyUpdateRequest {
        ProductId = BasketItemUpdateRequest.ProductId,
        NewQty = BasketItemUpdateRequest.Qty
      };
    }
  }
}
