using System.Collections.Generic;

namespace Agathas.Storefront.Controllers.DTOs {
  public class BasketQtyUpdateRequest {
    public BasketItemUpdateRequest[] Items { get; set; }
  }
}
