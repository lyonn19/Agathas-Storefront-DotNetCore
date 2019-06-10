using System;
using System.Collections.Generic;

using Web = Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Model.Shipping;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class DeliveryOptionRepository : Repository<DeliveryOption, int>,
                                                        IDeliveryOptionRepository {
    private readonly Web.IHttpContextAccessor _context;
    
    public DeliveryOptionRepository(IUnitOfWork uow, Web.IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
