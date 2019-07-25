using Microsoft.AspNetCore.Http;
using NHibernate;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Products;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class ProductRepository : Repository<Product, int>, IProductRepository {
    public ProductRepository(IUnitOfWork<IHttpContextAccessor> uow) : base(uow) { }

    public override void AppendCriteria(ICriteria criteria) {
      criteria.CreateAlias("Title", "ProductTitle");
      criteria.CreateAlias("ProductTitle.Category", "Category");
      criteria.CreateAlias("ProductTitle.Brand", "Brand");
      criteria.CreateAlias("ProductTitle.Color", "Color");
    }
  }
}
