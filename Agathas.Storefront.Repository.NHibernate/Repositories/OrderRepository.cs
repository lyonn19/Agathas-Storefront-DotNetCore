using System;
using System.Collections.Generic;

using Web = Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.UnitOfWork;
using Agathas.Storefront.Model.Orders;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class OrderRepository : Repository<Order, int>, IOrderRepository {
    private readonly Web.IHttpContextAccessor _context;
    
    public OrderRepository(IUnitOfWork uow, Web.IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
