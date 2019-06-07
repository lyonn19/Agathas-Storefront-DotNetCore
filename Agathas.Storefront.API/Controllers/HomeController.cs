using Microsoft.AspNetCore.Mvc;

using Agathas.Storefront.Controllers.ViewModels.ProductCatalog;
using Agathas.Storefront.Infrastructure.CookieStorage;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;

namespace Agathas.Storefront.API.Controllers { 
  [ApiController]
  public class HomeController : ProductCatalogBaseController {
    private readonly IProductCatalogService _productCatalogService;

    public HomeController(IProductCatalogService productCatalogService,
            ICookieStorageService cookieStorageService) : base(cookieStorageService, 
                                                            productCatalogService) {
      _productCatalogService = productCatalogService;
    }
    
    [Route("api/home")]
    [HttpGet]
    public ActionResult<HomePageView> Index() {
      var homePageView = new HomePageView();
      homePageView.Categories = base.GetCategories();
      homePageView.BasketSummary = base.GetBasketSummaryView();

      GetFeaturedProductsResponse response =
                      _productCatalogService.GetFeaturedProducts();
      homePageView.Products = response.Products;

      return homePageView;
    }
  }
}
