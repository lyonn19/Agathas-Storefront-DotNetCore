using System.Collections.Generic;

using AutoMapper;

using Agathas.Storefront.Models.Products;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Mapping {
  public static class ProductTitleMapper {
    public static IEnumerable<ProductSummaryView> ConvertToProductViews(
              this IEnumerable<ProductTitle> products, IMapper mapper) {
      return mapper.Map<IEnumerable<ProductTitle>, IEnumerable<ProductSummaryView>>(products);
    }

    public static ProductView ConvertToProductDetailView(this ProductTitle product, IMapper mapper) {
      return mapper.Map<ProductTitle, ProductView>(product);
    }
  }
}
