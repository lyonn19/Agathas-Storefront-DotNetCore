using System.Collections.Generic;

using AutoMapper;

using Agathas.Storefront.Models.Shipping;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Mapping {
  public static class DeliveryOptionMapper {
    public static IEnumerable<DeliveryOptionView> ConvertToDeliveryOptionViews(
              this IEnumerable<DeliveryOption> deliveryOptions, IMapper mapper) {
        return mapper.Map<IEnumerable<DeliveryOption>,
                          IEnumerable<DeliveryOptionView>>(deliveryOptions);
    }
  }

}
