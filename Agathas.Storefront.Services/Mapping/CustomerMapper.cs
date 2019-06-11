using AutoMapper;

using Agathas.Storefront.Models.Customers;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Mapping {
  public static class CustomerMapper {
    public static CustomerView ConvertToCustomerDetailView(
                                          this Customer customer, IMapper mapper) {
      return mapper.Map<Customer, CustomerView>(customer);
    }
  }

}
