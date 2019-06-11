using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Basket;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class BasketRepository : Repository<Basket, Guid>, IBasketRepository {
    private readonly IHttpContextAccessor _context;
    
    public BasketRepository(IUnitOfWork uow, IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
