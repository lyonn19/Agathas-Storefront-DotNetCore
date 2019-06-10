using System.Collections.Generic;
using System.Linq;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Querying;
using Agathas.Storefront.Infrastructure.Helpers;
using Agathas.Storefront.Model.Categories;
using Agathas.Storefront.Model.Products;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Mapping;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;

namespace Agathas.Storefront.Services.Implementations {
  public class ProductCatalogService : IProductCatalogService {
    private readonly IProductTitleRepository _productTitleRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductCatalogService(IProductTitleRepository productTitleRepository,
                                    IProductRepository productRepository,
                                    ICategoryRepository categoryRepository) {
      _productTitleRepository = productTitleRepository;
      _productRepository = productRepository;
      _categoryRepository = categoryRepository;
    }

    private IEnumerable<Product> GetAllProductsMatchingQueryAndSort(
            GetProductsByCategoryRequest request, Query productQuery) {
      var productsMatchingRefinement = _productRepository.FindBy(productQuery);

      switch (request.SortBy) {
        case ProductsSortBy.PriceLowToHigh:
          productsMatchingRefinement = productsMatchingRefinement.OrderBy(p => p.Price);
          break;
        case ProductsSortBy.PriceHighToLow:
          productsMatchingRefinement = productsMatchingRefinement
                  .OrderByDescending(p => p.Price);
          break;
      }
      return productsMatchingRefinement;
    }

    public GetFeaturedProductsResponse GetFeaturedProducts() {
      var response = new GetFeaturedProductsResponse();
      var productQuery = new Query();

      productQuery.OrderByProperty = new OrderByClause() { 
              Desc = true, PropertyName = 
                  PropertyNameHelper.ResolvePropertyName<ProductTitle>(pt => pt.Price) };

      response.Products = _productTitleRepository.FindBy(
              productQuery, 0, 6).ConvertToProductViews();

      return response;
    }

    public GetProductsByCategoryResponse GetProductsByCategory(
            GetProductsByCategoryRequest request) {
      GetProductsByCategoryResponse response;

      var productQuery = ProductSearchRequestQueryGenerator.CreateQueryFor(request);
      var productsMatchingRefinement = 
              GetAllProductsMatchingQueryAndSort(request, productQuery);

      response = productsMatchingRefinement.CreateProductSearchResultFrom(request);
      response.SelectedCategoryName =
              _categoryRepository.FindBy(request.CategoryId).Name;
      return response;
    }

    public GetProductResponse GetProduct(GetProductRequest request) {
      var response = new GetProductResponse();
      var productTitle = _productTitleRepository.FindBy(request.ProductId);

      response.Product = productTitle.ConvertToProductDetailView();
      return response;
    }

    public GetAllCategoriesResponse GetAllCategories() {
      var response = new GetAllCategoriesResponse();
      var categories = _categoryRepository.FindAll();
      response.Categories = categories.ConvertToCategoryViews();

      return response;
    }
  }
}
