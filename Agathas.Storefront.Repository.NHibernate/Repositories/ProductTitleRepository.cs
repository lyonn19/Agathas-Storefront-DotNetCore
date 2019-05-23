using System;
using System.Collections.Generic;

using Web = Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.UnitOfWork;
using Agathas.Storefront.Model.Products;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class ProductTitleRepository : Repository<ProductTitle, int>,
                                                        IProductTitleRepository {
    private readonly Web.IHttpContextAccessor _context;

    public ProductTitleRepository(IUnitOfWork uow, Web.IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
