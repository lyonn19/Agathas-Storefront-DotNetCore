using System.Collections.Generic;

using Agathas.Storefront.Services.Messaging.ProductCatalogService;

namespace Agathas.Storefront.Controllers.DTOs {
  public static class DtoMapper     {
    public static IList<ProductQtyUpdateRequest>
      ConvertToBasketItemUpdateRequests(
                      this BasketQtyUpdateRequest BasketQtyUpdateRequest) {
        return BasketQtyUpdateRequest.Items
                  .ConvertToBasketItemUpdateRequests();
    }

    public static IList<ProductQtyUpdateRequest>
      ConvertToBasketItemUpdateRequests(
                this BasketItemUpdateRequest[] BasketItemUpdateRequests) {
        int i = 0;
        IList<ProductQtyUpdateRequest> basketItemUpdateRequests =
                                            new List<ProductQtyUpdateRequest>();

        for (i = 0; i < BasketItemUpdateRequests.Length; i++) {
          basketItemUpdateRequests.Add(
              BasketItemUpdateRequests[i]
                    .ConvertToBasketItemUpdateRequest());
        }

        return basketItemUpdateRequests;
    }

    public static ProductQtyUpdateRequest ConvertToBasketItemUpdateRequest(
                  this BasketItemUpdateRequest BasketItemUpdateRequest) {
      return new ProductQtyUpdateRequest {
        ProductId = BasketItemUpdateRequest.ProductId,
        NewQty = BasketItemUpdateRequest.Qty
      };
    }
  }
}
