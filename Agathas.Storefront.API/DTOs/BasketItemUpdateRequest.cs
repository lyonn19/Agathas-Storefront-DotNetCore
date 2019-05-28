using System.Collections.Generic;

namespace Agathas.Storefront.Controllers.DTOs {
  public class BasketItemUpdateRequest {
    public int ProductId { get; set; }
    public int Qty { get; set; }
  }
}
