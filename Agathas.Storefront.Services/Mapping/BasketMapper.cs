using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;

using Agathas.Storefront.Models.Basket;
using Agathas.Storefront.Models.Orders;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Mapping {
  public static class BasketMapper {
    public static BasketView ConvertToBasketView(this Basket basket, IMapper mapper) {
      return mapper.Map<Basket, BasketView>(basket);
    }

    public static Order ConvertToOrder(this Basket basket) {
      var order = new Order();
      order.ShippingCharge = basket.DeliveryCost();
      order.ShippingService = basket.DeliveryOption.ShippingService;

      foreach (var item in basket.Items) {
        order.AddItem(item.Product, item.Qty);
      }
      return order;
    }
  }
}
