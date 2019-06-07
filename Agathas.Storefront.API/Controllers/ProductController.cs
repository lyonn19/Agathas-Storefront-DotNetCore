using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Mvc;

using Agathas.Storefront.Controllers.ViewModels.ProductCatalog;
using Agathas.Storefront.Infrastructure.Configuration;
using Agathas.Storefront.Infrastructure.CookieStorage;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.API.Controllers {
  [ApiController]
  public class ProductController : ProductCatalogBaseController {
    private readonly IProductCatalogService _productService;

    public ProductController(IProductCatalogService productService,
            ICookieStorageService cookieStorageService) : base(cookieStorageService, 
                                                            productService) {
      _productService = productService;
    }
    
    [Route("api/product")]
    [HttpGet("{categoryId}")]
    public ActionResult<ProductSearchResultView> GetProductsByCategory(int categoryId) {
      var productSearchRequest = GenerateInitialProductSearchRequestFrom(categoryId);
      var response = _productService.GetProductsByCategory(productSearchRequest);
      var productSearchResultView = GetProductSearchResultViewFrom(response);

      return productSearchResultView;
    }

    private ProductSearchResultView GetProductSearchResultViewFrom(
                                  GetProductsByCategoryResponse response) {
      var productSearchResultView = new ProductSearchResultView();
      productSearchResultView.BasketSummary = base.GetBasketSummaryView();
      productSearchResultView.Categories = base.GetCategories();
      productSearchResultView.CurrentPage = response.CurrentPage;
      productSearchResultView.NumberOfTitlesFound = response.NumberOfTitlesFound;
      productSearchResultView.Products = response.Products;
      productSearchResultView.RefinementGroups = response.RefinementGroups;
      productSearchResultView.SelectedCategory = response.SelectedCategory;
      productSearchResultView.SelectedCategoryName = response.SelectedCategoryName;
      productSearchResultView.TotalNumberOfPages = response.TotalNumberOfPages;
      return productSearchResultView;
    }

    private static GetProductsByCategoryRequest GenerateInitialProductSearchRequestFrom(
            int categoryId) {
      var productSearchRequest = new GetProductsByCategoryRequest();
      productSearchRequest.NumberOfResultsPerPage = int.Parse(ApplicationSettingsFactory
                                .GetApplicationSettings().NumberOfResultsPerPage);
      productSearchRequest.CategoryId = categoryId;
      productSearchRequest.Index = 1;
      productSearchRequest.SortBy = ProductsSortBy.PriceHighToLow;
      return productSearchRequest;
    }
    
    [Route("api/product")]
    [HttpPost("{categoryId}")]
    public ProductSearchResultView GetProductsByAjax(
            DTOs.ProductSearchRequest productSearchRequest) {
      
      var request = GenerateProductSearchRequestFrom(productSearchRequest);
      var response = _productService.GetProductsByCategory(request);
      var productSearchResultView = GetProductSearchResultViewFrom(response);

      return productSearchResultView;
    }

    private static GetProductsByCategoryRequest GenerateProductSearchRequestFrom(
            DTOs.ProductSearchRequest productSearchRequest) {

      var request = new GetProductsByCategoryRequest();

      request.NumberOfResultsPerPage = int.Parse(ApplicationSettingsFactory
                                          .GetApplicationSettings().NumberOfResultsPerPage);
      request.Index = productSearchRequest.Index;
      request.CategoryId = productSearchRequest.CategoryId;
      request.SortBy = productSearchRequest.SortBy;

      var refinementGroups = new List<RefinementGroup>();
      
      foreach (var refinementGroup in productSearchRequest.RefinementGroups) {
        switch ((RefinementGroupings) refinementGroup.GroupId) {
        case RefinementGroupings.brand:
          request.BrandIds = refinementGroup.SelectedRefinements;
          break;
        case RefinementGroupings.color:
          request.ColorIds = refinementGroup.SelectedRefinements;
          break;
        case RefinementGroupings.size:
          request.SizeIds = refinementGroup.SelectedRefinements;
          break;
        default:
          break;
        }
      }
      return request;
    }

    [Route("api/product")]
    [HttpGet]
    public ActionResult<ProductDetailView> Detail(int id) {
      var productDetailView = new ProductDetailView();
      var request = new GetProductRequest() { ProductId = id };
      var response = _productService.GetProduct(request);
      var productView = response.Product;
      
      productDetailView.Product = productView;
      productDetailView.BasketSummary = base.GetBasketSummaryView();
      productDetailView.Categories = base.GetCategories();

      return productDetailView;
    }
  }
}
