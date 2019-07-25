using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Products;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class ProductTitleRepository : Repository<ProductTitle, int>,
                                                        IProductTitleRepository {

    public ProductTitleRepository(IUnitOfWork<IHttpContextAccessor> uow) : base(uow) { }
  }
}
