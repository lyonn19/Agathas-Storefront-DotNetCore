using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Basket;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class BasketRepository : Repository<Basket, Guid>, IBasketRepository {    
    public BasketRepository(IUnitOfWork<IHttpContextAccessor> uow) : base(uow) { }
  }
}
