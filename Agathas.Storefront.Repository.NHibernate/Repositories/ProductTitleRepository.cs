using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Products;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class ProductTitleRepository : Repository<ProductTitle, int>,
                                                        IProductTitleRepository {
    private readonly IHttpContextAccessor _context;

    public ProductTitleRepository(IUnitOfWork uow, IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
