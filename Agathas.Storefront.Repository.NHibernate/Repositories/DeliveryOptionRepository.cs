﻿using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Shipping;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class DeliveryOptionRepository : Repository<DeliveryOption, int>,
                                                        IDeliveryOptionRepository {
    private readonly IHttpContextAccessor _context;
    
    public DeliveryOptionRepository(IUnitOfWork uow, IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
