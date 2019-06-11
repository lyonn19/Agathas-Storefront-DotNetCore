using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Agathas.Storefront.Infrastructure.CookieStorage;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.API.Controllers {
  [ApiController]
  public class ProductCatalogBaseController : BaseController {
    private readonly IProductCatalogService _productCatalogService;

    public ProductCatalogBaseController(
            ICookieStorageService cookieStorageService,
            IProductCatalogService productCatalogService) : base(cookieStorageService) {
      _productCatalogService = productCatalogService;
    }

    [HttpGet("categories")]
    public IEnumerable<CategoryView> GetCategories() {
      var response = _productCatalogService.GetAllCategories();
      return response.Categories;
    }
  }
}
