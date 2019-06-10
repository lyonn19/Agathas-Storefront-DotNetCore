using System;
using System.Collections.Generic;

using Web = Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Model.Basket;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class BasketRepository : Repository<Basket, Guid>, IBasketRepository {
    private readonly Web.IHttpContextAccessor _context;
    
    public BasketRepository(IUnitOfWork uow, Web.IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
