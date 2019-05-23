using System;
using System.Collections.Generic;

using Web = Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.UnitOfWork;
using Agathas.Storefront.Model.Products;
using NHibernate;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class ProductRepository : Repository<Product, int>, IProductRepository {
    private readonly Web.IHttpContextAccessor _context;

    public ProductRepository(IUnitOfWork uow, Web.IHttpContextAccessor context)
      : base(uow, context) { }

    public override void AppendCriteria(ICriteria criteria) {
      criteria.CreateAlias("Title", "ProductTitle");
      criteria.CreateAlias("ProductTitle.Category", "Category");
      criteria.CreateAlias("ProductTitle.Brand", "Brand");
      criteria.CreateAlias("ProductTitle.Color", "Color");
    }
  }
}
