using System.Collections.Generic;

namespace Agathas.Storefront.API.Controllers.DTOs {
  public class BasketQtyUpdateRequest {
    public BasketItemUpdateRequest[] Items { get; set; }
  }
}
