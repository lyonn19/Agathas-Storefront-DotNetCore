using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Categories;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class CategoryRepository : Repository<Category, int>, ICategoryRepository {  
    public CategoryRepository(IUnitOfWork<IHttpContextAccessor> uow) : base(uow) { }
  }
}
