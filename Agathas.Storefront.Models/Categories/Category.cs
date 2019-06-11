using System;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Products;

namespace Agathas.Storefront.Models.Categories {
  public class Category : EntityBase<int>, IAggregateRoot, IProductAttribute {               
    public string Name { get; set; }

    protected override void Validate() {
      throw new NotImplementedException();
    }
  }
}
