using System;
using System.Collections.Generic;

using AutoMapper;

using Agathas.Storefront.Models.Categories;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Mapping {
  public static class CategoryMapper {
    
    public static IEnumerable<CategoryView> ConvertToCategoryViews(
                                            this IEnumerable<Category> categories, IMapper mapper) {
      return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryView>>(categories);
    }
  }
}
