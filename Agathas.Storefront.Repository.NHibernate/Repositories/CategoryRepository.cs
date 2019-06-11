using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Categories;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class CategoryRepository : Repository<Category, int>, ICategoryRepository {
    private readonly IHttpContextAccessor _context;
  
    public CategoryRepository(IUnitOfWork uow, IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
