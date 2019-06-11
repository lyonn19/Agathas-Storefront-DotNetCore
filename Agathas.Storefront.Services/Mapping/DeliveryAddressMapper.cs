using AutoMapper;

using Agathas.Storefront.Models.Customers;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Mapping {
  public static class DeliveryAddressMapper {
    public static DeliveryAddressView ConvertToDeliveryAddressView(
                                            this DeliveryAddress deliveryAddress,
                                            IMapper mapper) {
      return mapper.Map<DeliveryAddress, DeliveryAddressView>(deliveryAddress);
    }
  }
}
