using System.Collections.Generic;

namespace Agathas.Storefront.API.Controllers.DTOs {
  public class BasketItemUpdateRequest {
    public int ProductId { get; set; }
    public int Qty { get; set; }
  }
}
