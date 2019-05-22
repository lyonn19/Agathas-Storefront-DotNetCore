using System;
using System.Collections.Generic;

using Agathas.Storefront.Infrastructure.Domain;

namespace Agathas.Storefront.Model.Categories {
  public interface ICategoryRepository : IReadOnlyRepository<Category,int> { }
}
