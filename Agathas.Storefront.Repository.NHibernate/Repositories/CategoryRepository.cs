using System;
using System.Collections.Generic;

using Web = Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.UnitOfWork;
using Agathas.Storefront.Model.Categories;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public class CategoryRepository : Repository<Category, int>, ICategoryRepository {
    private readonly Web.IHttpContextAccessor _context;
  
    public CategoryRepository(IUnitOfWork uow, Web.IHttpContextAccessor context)
      : base(uow, context) { }
  }
}
