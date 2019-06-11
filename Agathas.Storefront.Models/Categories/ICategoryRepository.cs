using Agathas.Storefront.Infrastructure.Domain;

namespace Agathas.Storefront.Models.Categories {
  public interface ICategoryRepository : IReadOnlyRepository<Category,int> { }
}
