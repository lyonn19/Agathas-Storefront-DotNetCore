using System.Collections.Generic;

using AutoMapper;

using Agathas.Storefront.Models.Orders;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Mapping {
  public static class OrderMapper {
    public static OrderView ConvertToOrderView(this Order order, IMapper mapper) {
      return mapper.Map<Order, OrderView>(order);
    }

    public static IEnumerable<OrderSummaryView> ConvertToOrderSummaryViews(
                                                  this IEnumerable<Order> orders,
                                                  IMapper mapper) {
      return mapper.Map<IEnumerable<Order>, IEnumerable<OrderSummaryView>>(orders);
    }
  }
}
