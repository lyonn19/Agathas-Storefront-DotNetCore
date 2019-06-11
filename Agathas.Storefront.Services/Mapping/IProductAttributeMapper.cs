using System.Collections.Generic;

using AutoMapper;

using Agathas.Storefront.Models.Products;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Mapping {
  public static class IProductAttributeMapper {
    public static RefinementGroup ConvertToRefinementGroup(
                        this IEnumerable<IProductAttribute> productAttributes,
                        RefinementGroupings refinementGroupType,
                        IMapper mapper) {
      var refinementGroup = new RefinementGroup() {
        Name = refinementGroupType.ToString(),
        GroupId = (int)refinementGroupType
      };

      refinementGroup.Refinements =
            mapper.Map<IEnumerable<IProductAttribute>, IEnumerable<Refinement>>
                                                            (productAttributes);

      return refinementGroup;
    }
  }
}
